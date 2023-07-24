using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class SidebarController : BasicController<Sidebar, SidebarDTO>
    {
        private ISidebarRepository _repository;

        public SidebarController(ISidebarRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
