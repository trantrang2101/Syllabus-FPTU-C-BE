using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class RoleSidebarController : BasicController<RoleSidebar, RoleSidebarDTO>
    {
        private IRoleSidebarRepository _repository;

        public RoleSidebarController(IRoleSidebarRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
