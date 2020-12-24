using Customer.Data.IRepositories;
using Customer.Domain.Dtos;
using Customer.Domain.Models;
using Customer.Domain.Queries;
using Customer.Service.Dxos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Service.Services
{
    public class GetManufacturerHandlerById : IRequestHandler<GetManufacturerQuery, ManufacturerDto>
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IManufacturerDxos _manufacturerDxos;
        public GetManufacturerHandlerById(IManufacturerRepository manufacturerRepository, IManufacturerDxos manufacturerDxos)
        {
            _manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
            _manufacturerDxos = manufacturerDxos ?? throw new ArgumentNullException(nameof(manufacturerDxos));
        }

        public async Task<ManufacturerDto> Handle(GetManufacturerQuery request, CancellationToken cancellationToken)
        {
            Manufacturer manufacturer = await _manufacturerRepository.GetManufacturerAsync(request.ManufacturerId);
            if (manufacturer == null)
            {
                throw new Exception("Please create a manufacturer!");
            }
            return _manufacturerDxos.MapManufacturerDto(manufacturer);
        }
    }
}
