using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class RoleController : BasicController<Role, RoleDTO>
    {
        private IRoleRepository _repository;

        public RoleController(IRoleRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
