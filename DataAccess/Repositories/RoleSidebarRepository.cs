using AutoMapper;
using BusinessObject.Models;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RoleSidebarRepository : BaseRespository<RoleSidebar, RoleSidebarDTO>, IRoleSidebarRepository
    {
        private static IRoleSidebarRepository _roleSidebarRepository;
        public RoleSidebarRepository(IMapper mapper, DatabaseContext context, IRoleSidebarRepository roleSidebarRepository) : base(mapper, context)
        {
            _roleSidebarRepository = roleSidebarRepository;
        }
    }
}
