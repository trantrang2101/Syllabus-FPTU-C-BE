using AutoMapper;
using BusinessObject.Models;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class AccountRepository : BaseRespository<Account, AccountDTO>, IAccountRepository
    {
        private static IAuthenticationRepository _authenticationRepository;

        public AccountRepository(IMapper mapper, DatabaseContext context, IAuthenticationRepository authenticationRepository) : base(mapper, context)
        {
            _authenticationRepository = authenticationRepository;
        }

        public AccountDTO Login(string gmail)
        {
            if (string.IsNullOrEmpty(gmail))
            {
                throw new Exception("Trường Email là trường bắt buộc");
            }
            Account? acc = table.Where(x => x.Email.ToLower() == gmail.ToLower())
                .Include("AccountRoles.Role.RoleSidebars.Sidebar").FirstOrDefault();
            if (acc == null)
            {
                throw new Exception("Không tìm thấy tài khoản");
            }
            AccountDTO account = _mapper.Map<AccountDTO>(acc);
            string token = _authenticationRepository.GetJwtToken(account);
            account.Token = token;
            return account;
        }

        public override AccountDTO Add(AccountDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Chưa truyền giá trị vào");
            }
            Account basic = _mapper.Map<Account>(dto);
            basic.AccountRoles = new List<AccountRole>();
            foreach (var item in dto.Roles)
            {
                basic.AccountRoles.Add(new AccountRole()
                {
                    RoleId = item.Id,
                });
            }
            Account saveBasic = table.Add(basic).Entity;
            _context.SaveChanges();
            return Get(saveBasic.Id);
        }

        public override List<AccountDTO> GetAll()
        {
            List<Account> products = table.Include("AccountRoles.Role").ToList();
            List<AccountDTO> dto = new List<AccountDTO>() { };
            if (products != null && products.Count() > 0)
            {
                return _mapper.Map<List<AccountDTO>>(products);
            }
            return dto;
        }

        public override AccountDTO Get(long id)
        {
            Account basic = table.Include("AccountRoles.Role").FirstOrDefault(x=>x.Id==id);
            if (basic == null || basic.Status == 0)
            {
                throw new Exception("Không tìm thấy hoặc đã bị xóa");
            }
            return _mapper.Map<AccountDTO>(basic);
        }
    }
}
