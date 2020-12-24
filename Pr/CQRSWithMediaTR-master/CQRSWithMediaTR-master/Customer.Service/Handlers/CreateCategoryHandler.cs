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
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>, IRequestHandler<CreateCategoryCommandBackGround, Unit>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediator _mediator;
        public CreateCategoryHandler(IMediator mediator, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.CategoryExistAsync(request.CategoryName))
            {
                throw new Exception($"This category '{request.CategoryName}' is already existed!");
            }
            Domain.Models.Category category = new Domain.Models.Category(request.CategoryName);
            _categoryRepository.Add(category);
            CategoryDto result = await Task.Run(() => {
                if (_categoryRepository.SaveChangesAsync().Result == 0)
                {
                    return new CategoryDto();
                }
                return new CategoryDto();
            });
            return result;
        }

        public async Task<Unit> Handle(CreateCategoryCommandBackGround request, CancellationToken cancellationToken)
        {
            await Handle(new CreateCategoryCommand() { CategoryName=request.CategoryName}, cancellationToken);
            return new Unit();
        }
    }
}
