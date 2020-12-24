using AutoMapper;
using Customer.Domain.Dtos;
using Customer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Customer.Service.Dxos
{
    public class ManufacturerDxos : IManufacturerDxos
    {
        private readonly IMapper _mapper;

        public ManufacturerDxos()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Manufacturer, ManufacturerDto>()
                .ForMember(dst => dst.ManufacturerId, opt => opt.MapFrom(src => src.ManufacturerId))
                .ForMember(dst => dst.ManufacturerName, opt => opt.MapFrom(src => src.ManufacturerName));
            });
            _mapper = config.CreateMapper();
        }
        public ManufacturerDto MapManufacturerDto(Manufacturer manufacturer)
        {
            return _mapper.Map<Manufacturer, ManufacturerDto>(manufacturer);
        }

        public List<ManufacturerDto> MapListManufacturerDto(IEnumerable<Manufacturer> manufacturers)
        {
            List<Manufacturer> result = manufacturers.ToList();
            return _mapper.Map<IEnumerable<Manufacturer>, List<ManufacturerDto>>(result);
        }
    }
}
