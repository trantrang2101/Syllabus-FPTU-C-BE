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
    public class SidebarRepository : BaseRespository<Sidebar, SidebarDTO>, ISidebarRepository
    {
        private static ISidebarRepository _sidebarRepository;
        public SidebarRepository(IMapper mapper, DatabaseContext context, ISidebarRepository sidebarRepository) : base(mapper, context)
        {
            _sidebarRepository = sidebarRepository;
        }
    }
}
