using BusinessObject.Models;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AccountRoleController : BasicController<AccountRole, AccountRoleDTO>
    {
        private IAccountRoleRepository _accountRoleRepository;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository) : base(accountRoleRepository) 
        {
            _accountRoleRepository = accountRoleRepository;
        }
    }
}
