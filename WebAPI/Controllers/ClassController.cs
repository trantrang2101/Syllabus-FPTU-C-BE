using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class ClassController : BasicController<Class, ClassDTO>
    {
        private IClassRepository _repository;

        public ClassController(IClassRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
