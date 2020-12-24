using Customer.Data.IRepositories;
using Customer.Domain.Commands;
using Customer.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Service.Services
{
    public class CreateManufacturerHandler : IRequestHandler<CreateManufacturerCommand, ManufacturerDto>, IRequestHandler<CreateManufacturerCommandBackGround, Unit>
    {
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IMediator _mediator;
        public CreateManufacturerHandler(IMediator mediator, IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<ManufacturerDto> Handle(CreateManufacturerCommand request, CancellationToken cancellationToken)
        {
            if (await _manufacturerRepository.ManufacturerExistAsync(request.ManufacturerName))
            {
                throw new Exception($"This manufacturer '{request.ManufacturerName}' is already existed!");
            }
            Domain.Models.Manufacturer manufacturer = new Domain.Models.Manufacturer(request.ManufacturerName);
            _manufacturerRepository.Add(manufacturer);
            ManufacturerDto result = await Task.Run(() => {
                if (_manufacturerRepository.SaveChangesAsync().Result == 0)
                {
                    return new ManufacturerDto();
                }
                return new ManufacturerDto();
            });
            return result;
        }

        async Task<Unit> IRequestHandler<CreateManufacturerCommandBackGround, Unit>.Handle(CreateManufacturerCommandBackGround request, CancellationToken cancellationToken)
        {
            await Handle(new CreateManufacturerCommand() { ManufacturerName=request.ManufacturerName}, cancellationToken);
            return new Unit();
        }
    }
}
