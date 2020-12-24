using Customer.Data.IRepositories;
using Customer.Domain.Dtos;
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
    public class GetCategoryHandlerById : IRequestHandler<GetCategoryQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoriesDxos _categoriesDxos;
        public GetCategoryHandlerById(ICategoryRepository categoryRepository, ICategoriesDxos categoriesDxos)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _categoriesDxos = categoriesDxos ?? throw new ArgumentNullException(nameof(categoriesDxos));
        }
        public async Task<CategoryDto> Handle(GetCategoryQuery containGoodQuery, CancellationToken cancellationToken)
        {
            Domain.Models.Category categoryDto = await _categoryRepository.GetCategoryAsync(containGoodQuery.CategoryId);
            if (categoryDto == null)
            {
                throw new Exception("Please create a category!");
            }
            return _categoriesDxos.MapCategoryDto(categoryDto);
        }
    }    
}
