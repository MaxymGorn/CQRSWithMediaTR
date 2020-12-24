using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Maxs.Services
{
    public class BasicService
    {
        private readonly IRestClient restClient;
        private readonly ILogger<BasicService> logger;
        public BasicService(IRestClient restClient, ILogger<BasicService> logger)
        {
            this.restClient = restClient;
            this.logger = logger;
        }
        public BasicService(IRestClient restClient, ILogger<BasicService> logger, string Header)
        {
            restClient = new RestClient(Header);
            this.restClient = restClient;
            this.logger = logger;
        }
        protected async Task<T> GetDataAsync<T>(IRestRequest restRequest)
        {
            T response = default;
            Stopwatch stopWatch = new Stopwatch();
            try
            {
                stopWatch.Start();
                response = await restClient.GetAsync<T>(restRequest);
                stopWatch.Stop();
            }
            catch (Exception)
            {

            }
            finally
            {
                LogRequest(restRequest, response, stopWatch.ElapsedMilliseconds);
            }
            return response;
        }
        protected async Task<T> PostDataAsync<T>(IRestRequest restRequest)
        {
            T response = default;
            Stopwatch stopWatch = new Stopwatch();
            try
            {
                stopWatch.Start();
                response = await restClient.PostAsync<T>(restRequest);
                stopWatch.Stop();
            }
            catch (Exception)
            {

            }
            finally
            {
                LogRequest(restRequest, response, stopWatch.ElapsedMilliseconds);
            }
            return response;
        }
        private void LogRequest<T>(IRestRequest request, T responseToLog, long durationMs)
        {
            var requestToLog = new
            {
                resource = request.Resource,
                // Parameters are custom anonymous objects in order to have the parameter type as a nice string
                // otherwise it will just show the enum value
                parameters = request.Parameters.Select(parameter => new
                {
                    name = parameter.Name,
                    value = parameter.Value,
                    type = parameter.Type.ToString()
                }),
                // ToString() here to have the method as a nice string otherwise it will just show the enum value
                method = request.Method.ToString(),
                // This will generate the actual Uri used in the request
                uri = restClient.BuildUri(request),
            };
            logger.LogInformation(string.Format("Request completed in {0} ms, Request: {1}, Response: {2}",
                    durationMs,
                    JsonConvert.SerializeObject(requestToLog),
                    JsonConvert.SerializeObject(responseToLog)));
        }
        [Obsolete]
        protected async Task<IRestResponse> PostAsync(IRestRequest restRequest)
        {
            var cancellationTokenSource = new CancellationTokenSource();

            return await restClient.ExecuteTaskAsync(restRequest, cancellationTokenSource.Token);
        }
    }
}
