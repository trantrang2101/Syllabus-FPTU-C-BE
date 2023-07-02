using AutoMapper;
using BusinessObject.Models;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class AccountReponsitory : BaseResponsitory<Account, AccountDTO>, IAccountReponsitory
    {
        private static IAuthenticationRepository _authenticationRepository;

        public AccountReponsitory(IMapper mapper, DatabaseContext context, IAuthenticationRepository authenticationRepository) : base(mapper, context)
        {
            _authenticationRepository = authenticationRepository;
        }

        public string Login(string gmail)
        {
            if (string.IsNullOrEmpty(gmail))
            {
                throw new Exception("Not fill Email");
            }
            Account acc = table.FirstOrDefault(x => x.Email.ToLower() == gmail.ToLower());
            if (acc == null)
            {
                throw new Exception("Not Found Account");
            }
            AccountDTO account = _mapper.Map<AccountDTO>(acc);
            string token = _authenticationRepository.GetJwtToken(account);
            return token;
        }

        public override List<AccountDTO> GetAll()
        {
            List<Account> products = _context.Accounts.Include(x=>x.AccountRoles).ToList();
            List<AccountDTO> dto = new List<AccountDTO>() { };
            if (products != null && products.Count() > 0)
            {
                return _mapper.Map<List<AccountDTO>>(products);
            }
            return dto;
        }
    }
}
