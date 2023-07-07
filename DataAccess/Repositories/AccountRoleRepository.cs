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
    public class AccountRoleRepository : BaseResponsitory<AccountRole, AccountRoleDTO>, IAccountRoleRepository
    {
        private static IAccountRoleRepository _accountRoleRepository;

        public AccountRoleRepository(IMapper mapper, DatabaseContext context, IAccountRoleRepository accountRoleRepository) : base(mapper, context)
        {
            _accountRoleRepository = accountRoleRepository;
        }
    }
}
