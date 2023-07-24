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
    public class RoleRepository : BaseRespository<Role, RoleDTO>, IRoleRepository
    {
        public RoleRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
        public override List<RoleDTO> GetAll()
        {
            List<Role> products = _context.Roles.Include(x => x.RoleSidebars).ThenInclude(x => x.Sidebar).ToList();
            List<RoleDTO> dto = new List<RoleDTO>() { };
            if (products != null && products.Count() > 0)
            {
                return _mapper.Map<List<RoleDTO>>(products);
            }
            return dto;
        }
    }
}
