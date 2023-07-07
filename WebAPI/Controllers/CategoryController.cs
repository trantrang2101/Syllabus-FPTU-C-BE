using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;

namespace WebAPI.Controllers
{
    public class CategoryController : BasicController<Category, CategoryDTO>
    {
        private ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }

    }
}
