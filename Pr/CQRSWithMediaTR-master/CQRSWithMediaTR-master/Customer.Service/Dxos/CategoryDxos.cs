using AutoMapper;
using Customer.Domain.Dtos;
using Customer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Service.Dxos
{
    public class CategoryDxos: ICategoriesDxos
    {
        private readonly IMapper _mapper;

        public CategoryDxos()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDto>()
                .ForMember(dst => dst.CategoryName, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dst => dst.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
            });
            _mapper = config.CreateMapper();
        }


        public List<CategoryDto> MapListCategoryDto(IEnumerable<Category> categories)
        {
            List<Category> result = categories.ToList();
            return _mapper.Map<IEnumerable<Category>, List<CategoryDto>>(result);
        }
        public CategoryDto MapCategoryDto(Category categories)
        {
            return _mapper.Map<Category, CategoryDto>(categories);
        }
    }
}
