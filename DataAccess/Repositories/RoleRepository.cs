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
    public class RoleRepository : BaseRespository<Role, RoleDTO>, IRoleRepository
    {
        private static IRoleRepository _roleRepository;
        public RoleRepository(IMapper mapper, DatabaseContext context, IRoleRepository roleRepository) : base(mapper, context)
        {
            _roleRepository = roleRepository;
        }
    }
}
