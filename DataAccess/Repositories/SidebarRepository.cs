using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SidebarRepository : BaseRespository<Sidebar, SidebarDTO>, ISidebarRepository
    {
        public SidebarRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }

        public override SidebarDTO Get(long id)
        {
            var sidebar = _context.Sidebars.Include(s => s.Parent).Include(sidebar => sidebar.RoleSidebars).ThenInclude(sidebar => sidebar.Role).FirstOrDefault(s => s.Id == id);
            return _mapper.Map<SidebarDTO>(sidebar);

        }

        public override List<SidebarDTO> GetAll()
        {
            var sidebars = _context.Sidebars.Include(s => s.Parent).Include(sidebar => sidebar.RoleSidebars).ThenInclude(sidebar => sidebar.Role).ToList();
            return _mapper.Map<List<SidebarDTO>>(sidebars);
        }
    }
}
