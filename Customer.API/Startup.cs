using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Customer.Data;
using Customer.Data.IRepositories;
using Customer.Data.Repositories;
using Customer.Domain.Commands;
using Customer.Domain.Dtos;
using Customer.Domain.PipelineBehaviours;
using Customer.Domain.Queries;
using Customer.Service.Dxos;
using Customer.Service.Services;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.MediatR;
using Hangfire.MemoryStorage;
using MediatR;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RestSharp;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.File;
using Shop.Data.Interfaces;
using Shop.Data.Repositories;
using Shop.Domain.Data_Transfer_Objects;
using Shop.Domain.Queries;
using Shop.Maxs;
using Shop.Maxs.Interfaces;
using Shop.Maxs.Services;
using Shop.Service.Data_Exchange_Objects;
using Shop.Service.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace Customer.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            // Read configuration and combine appsettings.json and appsettings.env.json by environment of deployment
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            MySettings settings = Configuration.GetSection("MySettings").Get<MySettings>();
            services.AddTransient<IRestClient>(service => ActivatorUtilities.CreateInstance<RestClient>
            (service, settings.Header));
            services.AddSingleton<IShopService>(service => ActivatorUtilities.CreateInstance<ShopService>
            (service, settings.Header));
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CustomerDbContext>(options =>
                {
                    options.UseSqlServer(connectionString,
                        sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null);
                        });
                });
            Assembly assembly = Assembly.GetExecutingAssembly();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Maxs Gorn API",
                    Description = "Example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Maxs Gorn",
                        Email = "maximus@gmail.com"
                    }
                });
                string xfile = $"{assembly.GetName().Name}.xml";
                string xpath = Path.Combine(AppContext.BaseDirectory, xfile);
                c.IncludeXmlComments(xpath);
            });

            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
                    .AddCertificate(options => // code from ASP.NET Core sample
        {
                        options.AllowedCertificateTypes = CertificateTypes.All;
                        options.Events = new CertificateAuthenticationEvents
                        {
                            OnCertificateValidated = context =>
                            {
                                var validationService =
                                    context.HttpContext.RequestServices.GetService<MyCertificateValidationService>();

                                if (validationService.ValidateCertificate(context.ClientCertificate))
                                {
                                    var claims = new[]
                                    {
                            new Claim(ClaimTypes.NameIdentifier, context.ClientCertificate.Subject, ClaimValueTypes.String, context.Options.ClaimsIssuer),
                            new Claim(ClaimTypes.Name, context.ClientCertificate.Subject, ClaimValueTypes.String, context.Options.ClaimsIssuer)
                                    };

                                    context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
                                    context.Success();
                                }
                                else
                                {
                                    context.Fail("invalid cert");
                                }

                                return Task.CompletedTask;
                            }
                        };
                    });


            services.AddScoped<IPredicanteGenerator, PredicanteGenerator>();
            services.AddScoped<ICodeEngine, CodeEngine>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IGoodRepository, GoodRepository>();
            services.AddScoped<IManufacturerRepository, ManufacturerRepository>();

            services.AddScoped<IGoodsDxos, GoodsDxos>();
            services.AddScoped<ICategoriesDxos, CategoryDxos>();
            services.AddScoped<IManufacturerDxos, ManufacturerDxos>();
            
            services.AddTransient<IRequestHandler<GetListGoodsQuery, List<GoodsDto>>, GetGoodsHandler>();    
            services.AddTransient<IRequestHandler<CreateGoodsCommand, GoodsDto>, CreateGoodsHandler>();
            services.AddTransient<IRequestHandler<CreateGoodsCommandBackGround, Unit>, CreateGoodsHandler>();
            
            services.AddTransient<IRequestHandler<GetCategoryQuery, CategoryDto>, GetCategoryHandlerById>();
            services.AddTransient<IRequestHandler<GetListCategoryQuery, List<CategoryDto>>, GetCategoryHandler>();
            services.AddTransient<IRequestHandler<CreateCategoryCommandBackGround, Unit>, CreateCategoryHandler>();
            services.AddTransient<IRequestHandler<CreateCategoryCommand, CategoryDto>, CreateCategoryHandler>();

            services.AddTransient<IRequestHandler<GetListManufacturerQuery, List<ManufacturerDto>>, GetManufacturerHandler>();
            services.AddTransient<IRequestHandler<CreateManufacturerCommand, ManufacturerDto>, CreateManufacturerHandler>();
            services.AddTransient<IRequestHandler<CreateManufacturerCommandBackGround, Unit>, CreateManufacturerHandler>();
            services.AddTransient<IRequestHandler<GetManufacturerQuery, ManufacturerDto>, GetManufacturerHandlerById>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerDxos, CustomerDxos>();
            services.AddTransient<IRequestHandler<CreateCustomerCommand, CustomerDto>, CreateCustomerHandler>();
            services.AddTransient<IRequestHandler<GetCustomerQuery, CustomerDto>, GetCustomerHandler > ();
            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            #region Logging settings
            long sizeLimit = 10000000;
            long.TryParse(Configuration["Logging:SizeLimit"], out sizeLimit);
            int fileLimit = 3;
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            int.TryParse(Configuration["Logging:FileCountLimit"], out fileLimit);
            Serilog.Core.Logger serilogLogger = new LoggerConfiguration()
                 .Enrich.FromLogContext()
                 .Enrich.WithExceptionDetails()
                 .Enrich.WithMachineName()
                 .WriteTo.Elasticsearch(ConfigureElasticSink(new Uri(Configuration["ElasticConfiguration:Uri"]), environment, assembly))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(Configuration)
            //.WriteTo.File(Configuration["Logging:LogPath"], shared: true, rollOnFileSizeLimit: true, 
            //fileSizeLimitBytes: sizeLimit, retainedFileCountLimit: fileLimit, rollingInterval: RollingInterval.Infinite, 
            //outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();
            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddSerilog(logger: serilogLogger, dispose: true);
            });
            services.AddHangfire(config =>
            {
                config.UseMediatR();
                config.UseMemoryStorage();
            });
            //services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
            services.AddHangfireServer();
            #endregion

        }
        private static ElasticsearchSinkOptions ConfigureElasticSink(Uri Url, string environment, Assembly assembly)
        {
            return new ElasticsearchSinkOptions(Url)
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{assembly.GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            };
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                IsReadOnlyFunc = (DashboardContext context) => true,
                Authorization = new[] { new MyAuthorizationFilter() }
            });
            BackgroundJobServerOptions optionsHangfireServer = new BackgroundJobServerOptions
            {
                WorkerCount = 2,
                ServerName = $"{Environment.MachineName}.{Guid.NewGuid()}",
                SchedulePollingInterval = TimeSpan.FromMilliseconds(5000)
            };
            app.UseHangfireServer(optionsHangfireServer);
            //You may would disable auto migrate if needed
            Task.Run(async ()=>
            {
                await app.UseAutoMigrateDatabaseAsync<CustomerDbContext>();
            }); 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.RoutePrefix = string.Empty; c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1"); });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            //return httpContext.User.Identity.IsAuthenticated;
            return true;
        }
    }
}