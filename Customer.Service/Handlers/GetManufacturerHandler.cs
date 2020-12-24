using Customer.Data.IRepositories;
using Customer.Domain.Dtos;
using Customer.Domain.Models;
using Customer.Domain.Queries;
using Customer.Service.Dxos;
using LinqKit;
using MediatR;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Service.Services
{
    public class GetManufacturerHandler : IRequestHandler<GetListManufacturerQuery, List<ManufacturerDto>>
    {
        private readonly IMediator mediator;
        private readonly IManufacturerRepository manufacturerRepository;
        private readonly IManufacturerDxos manufacturerDxos;
        private readonly ILogger _logger;
        private readonly IPredicanteGenerator _predicanteGenerator;
        private readonly ICodeEngine _codeGenerator;
        public GetManufacturerHandler(IPredicanteGenerator predicanteGenerator, ICodeEngine codeGenerator, IMediator mediator, IManufacturerRepository manufacturerRepository,
        IManufacturerDxos manufacturerDxos, ILogger<GetManufacturerHandler> logger)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
            this.manufacturerDxos = manufacturerDxos ?? throw new ArgumentNullException(nameof(manufacturerDxos));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._predicanteGenerator = predicanteGenerator?? throw new ArgumentNullException(nameof(predicanteGenerator));
            this._codeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
        }

        public async Task<List<ManufacturerDto>> Handle(GetListManufacturerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                dynamic predicate = PredicateBuilder.New<Manufacturer>(true);
                if (request.Props != null)
                {
                    _logger.LogWarning($"Find: { request.Props.Count} my predicante elements!");
                    string code = _predicanteGenerator.GenerateCode(typeof(Manufacturer), request.Props);
                    var result = await _codeGenerator.RunCodeGetDataAsync(code, ScriptOptions.Default.AddReferences(Assembly.GetExecutingAssembly()));
                    predicate = result;
                }
                IEnumerable<Manufacturer> manufacturers = await manufacturerRepository.GetListAsync(predicate);
                _logger.LogInformation($"Count manufacturers: {manufacturers.ToList().Count}");
                return manufacturerDxos.MapListManufacturerDto(manufacturers);
            }
            catch (CompilationErrorException error)
            {
                throw new Exception($"Error predicante in: {error.Message}");
            }
            catch (Exception error)
            {
                return new List<ManufacturerDto>();
            }
        }
    }
}
