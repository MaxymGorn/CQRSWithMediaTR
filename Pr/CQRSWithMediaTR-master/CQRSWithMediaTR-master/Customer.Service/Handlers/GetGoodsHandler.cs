using Customer.Domain.Models;
using Customer.Service.Services;
using LinqKit;
using MediatR;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Logging;
using Shop.Data.Interfaces;
using Shop.Domain.Data_Transfer_Objects;
using Shop.Domain.Queries;
using Shop.Service.Data_Exchange_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Service.Services
{
    public class GetGoodsHandler : IRequestHandler<GetListGoodsQuery, List<GoodsDto>>
    {
        private readonly IMediator mediator;
        private readonly IGoodRepository goodRepository;
        private readonly IGoodsDxos goodsDxos;
        private readonly ILogger _logger;
        private readonly IPredicanteGenerator _predicanteGenerator;
        private readonly ICodeEngine _codeGenerator;
        public GetGoodsHandler(IPredicanteGenerator predicanteGenerator, ICodeEngine codeGenerator, IMediator mediator, IGoodRepository goodRepository, IGoodsDxos goodsDxos, ILogger<GetGoodsHandler> logger)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.goodRepository = goodRepository ?? throw new ArgumentNullException(nameof(goodRepository));
            this.goodsDxos = goodsDxos ?? throw new ArgumentNullException(nameof(goodsDxos));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._predicanteGenerator = predicanteGenerator ?? throw new ArgumentNullException(nameof(predicanteGenerator));
            this._codeGenerator = codeGenerator ?? throw new ArgumentNullException(nameof(codeGenerator));
        }

        public async Task<List<GoodsDto>> Handle(GetListGoodsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                dynamic predicate = PredicateBuilder.New<Good>(true);
                if (request.Props != null)
                {
                    _logger.LogWarning($"Find: { request.Props.Count} my predicante elements!");
                    string code = _predicanteGenerator.GenerateCode(typeof(Good), request.Props);
                    var result = await _codeGenerator.RunCodeGetDataAsync(code, ScriptOptions.Default.AddReferences(Assembly.GetExecutingAssembly()));
                    predicate = result;
                }
                IEnumerable<Good> goodsDto = await goodRepository.GetListAsync(predicate);
                _logger.LogInformation($"Count goods: {goodsDto.ToList().Count}");
                return goodsDxos.MapListGoodDto(goodsDto);
            }
            catch (CompilationErrorException error)
            {
                throw new Exception($"Error predicante in: {error.Message}");
            }
            catch (Exception error)
            {
                return new List<GoodsDto>();
            }
        }
    }
}
