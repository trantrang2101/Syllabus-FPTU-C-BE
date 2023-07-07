using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CategoryRepository : BaseRespository<Category, CategoryDTO>, ICategoryRepository
    {
        private static ICategoryRepository _categoryRepository;

        public CategoryRepository(IMapper mapper, DatabaseContext context, ICategoryRepository categoryRepository) : base(mapper, context)
        {
            _categoryRepository = categoryRepository;
        }
    }
}
