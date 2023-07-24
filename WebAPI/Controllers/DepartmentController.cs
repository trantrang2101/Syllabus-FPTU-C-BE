using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class DepartmentController : BasicController<Department,DepartmentDTO>
    {
        private IDepartmentRepository _repository;

        public DepartmentController(IDepartmentRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
