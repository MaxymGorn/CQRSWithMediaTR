using Customer.Data.IRepositories;
using Customer.Domain.Dtos;
using Customer.Domain.Models;
using Customer.Domain.Other;
using Customer.Domain.Queries;
using Customer.Service.Dxos;
using LinqKit;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CSharp;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Service.Services
{
    public class GetCategoryHandler : IRequestHandler<GetListCategoryQuery, List<CategoryDto>>
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly ICategoriesDxos _categoriesDxos;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPredicanteGenerator _predicanteGenerator;
        private readonly ICodeEngine _codeGenerator;
        public GetCategoryHandler(IPredicanteGenerator predicanteGenerator, ICodeEngine codeGenerator,
            IMediator mediator, ILogger<GetCategoryHandler> logger,
            ICategoryRepository categoryRepository, ICategoriesDxos categoriesDxos)
        {
            this._mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this._categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._categoriesDxos = categoriesDxos?? throw new ArgumentNullException(nameof(categoriesDxos));
            this._predicanteGenerator = predicanteGenerator ?? throw new ArgumentNullException(nameof(predicanteGenerator));
            this._codeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
        }

        public async Task<List<CategoryDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                dynamic predicate = PredicateBuilder.New<Category>(true);
                if (request.Props != null)
                {
                    _logger.LogWarning($"Find: { request.Props.Count} my predicante elements!");
                    string code = _predicanteGenerator.GenerateCode(typeof(Category), request.Props);
                    var result =  await _codeGenerator.RunCodeGetDataAsync(code, ScriptOptions.Default.AddReferences(Assembly.GetExecutingAssembly()));
                    predicate = result;
                }
                IEnumerable<Category> categoryDto = await _categoryRepository.GetListAsync(predicate);
                _logger.LogInformation($"Count category: {categoryDto.ToList().Count()}");
                return _categoriesDxos.MapListCategoryDto(categoryDto); }
            catch (CompilationErrorException error)
            {
                throw new Exception($"Error predicante in: {error.Message}");
            }
            catch (Exception error)
            {
                return new List<CategoryDto>();
            }
        }
    }
}
