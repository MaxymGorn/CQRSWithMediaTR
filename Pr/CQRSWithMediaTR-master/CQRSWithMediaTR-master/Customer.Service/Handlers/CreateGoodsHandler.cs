using Customer.Domain.Commands;
using Customer.Domain.Dtos;
using Customer.Domain.Extension;
using Customer.Domain.Queries;
using MediatR;
using Shop.Data.Interfaces;
using Shop.Domain.Data_Transfer_Objects;
using Shop.Service.Data_Exchange_Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Service.Services
{
    public class CreateGoodsHandler : IRequestHandler<CreateGoodsCommand, GoodsDto>, IRequestHandler<CreateGoodsCommandBackGround, Unit>
    {
        private readonly IGoodRepository _goodRepository;
        private readonly IMediator _mediator;

        public CreateGoodsHandler(IGoodRepository goodRepository, IMediator mediator)
        {
            _goodRepository = goodRepository ?? throw new ArgumentNullException(nameof(goodRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<GoodsDto> Handle(CreateGoodsCommand request, CancellationToken cancellationToken)
        {
            CategoryDto categorySend=default;
            ManufacturerDto manufacturerSend = default;
            try
            {
                manufacturerSend = await _mediator.Send(new GetManufacturerQuery(request.ManufacturerId));
            }
            catch (Exception)
            {

            }
            try
            {
                categorySend = await _mediator.Send(new GetCategoryQuery(request.CategoryId));
            }
            catch (Exception)
            {

            }
            if (categorySend == null && manufacturerSend == null)
            {
                throw new Exception($"This manufacturer id '{request.ManufacturerId}' not existed, this category id '{request.CategoryId}' not existed!");
            }
            else if (categorySend == null)
            {
                throw new Exception($"This category id '{request.CategoryId}' not existed!");
            }
            else if (manufacturerSend == null)
            {
                throw new Exception($"This manufacturer id '{request.ManufacturerId}' not existed!");
            }
            if (await _goodRepository.GoodExistAsync(request.GoodName))
            {
                throw new Exception($"This goods '{request.GoodName}' is already existed!");
            }
            Domain.Models.Good customer = new Domain.Models.Good(request.GoodName, request.ManufacturerId, request.CategoryId, request.Price, request.GoodCount);
            _goodRepository.Add(customer);
            GoodsDto result = await Task.Run(()=> {
                if (_goodRepository.SaveChangesAsync().Result == 0)
                {
                    return new GoodsDto();
                }
                return new GoodsDto();
            });
            return result;
        }

        public async Task<Unit> Handle(CreateGoodsCommandBackGround request, CancellationToken cancellationToken)
        {
            await Handle(new CreateGoodsCommand() 
            {
                CategoryId=request.CategoryId, 
                GoodCount=request.GoodCount, 
                GoodName=request.GoodName, 
                ManufacturerId=request.ManufacturerId, 
                Price=request.Price 
            }, cancellationToken);
            return new Unit();
        }
    }
}
