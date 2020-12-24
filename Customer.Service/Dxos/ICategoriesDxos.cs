using Customer.Domain.Dtos;
using Customer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Service.Dxos
{
    public interface ICategoriesDxos
    {
        List<CategoryDto> MapListCategoryDto(IEnumerable<Category> categories);
        CategoryDto MapCategoryDto(Category categories);
    }
}
