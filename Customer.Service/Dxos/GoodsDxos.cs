using AutoMapper;
using Customer.Domain.Models;
using Shop.Domain.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service.Data_Exchange_Objects
{
    public class GoodsDxos: IGoodsDxos
    {
        private readonly IMapper _mapper;

        public GoodsDxos()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Good, GoodsDto>()
                .ForMember(dst => dst.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dst => dst.GoodId, opt => opt.MapFrom(src => src.GoodId))
                .ForMember(dst => dst.GoodName, opt => opt.MapFrom(src => src.GoodName))
                .ForMember(dst => dst.ManufacturerId, opt => opt.MapFrom(src => src.ManufacturerId))
                .ForMember(dst => dst.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dst => dst.GoodCount, opt => opt.MapFrom(src => src.GoodCount));
            });
            _mapper = config.CreateMapper();
        }

        public List<GoodsDto> MapListGoodDto(IEnumerable<Good> goods)
        {
            List<Good> result = goods.ToList();
            return _mapper.Map<IEnumerable<Good>, List<GoodsDto>>(result);
        }
    }
}
