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
    public class AccountRoleRepository : BaseRespository<AccountRole, AccountRoleDTO>, IAccountRoleRepository 
    {
        public AccountRoleRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }
}
