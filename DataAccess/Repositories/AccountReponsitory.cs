using AutoMapper;
using BusinessObject.Models;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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
                throw new Exception("Trường Email là trường bắt buộc");
            }
            Account? acc = table.Where(x => x.Email.ToLower() == gmail.ToLower())
                .Include(x => x.AccountRoles).ThenInclude(x => x.Role).FirstOrDefault();
            if (acc == null)
            {
                throw new Exception("Không tìm thấy tài khoản");
            }
            AccountDTO account = _mapper.Map<AccountDTO>(acc);
            string token = _authenticationRepository.GetJwtToken(account);
            return token;
        }

        public override List<AccountDTO> GetAll()
        {
            List<Account> products = _context.Accounts.Include(x => x.AccountRoles).ThenInclude(x => x.Role).ToList();
            List<AccountDTO> dto = new List<AccountDTO>() { };
            if (products != null && products.Count() > 0)
            {
                return _mapper.Map<List<AccountDTO>>(products);
            }
            return dto;
        }

        public override AccountDTO Get(long id)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Id == id);
            var dto = new AccountDTO();
            if (account != null)
            {
                return _mapper.Map<AccountDTO>(account);
            }
            return dto;
        }

        public override AccountDTO Update(AccountDTO dto)
        {
            var account = _mapper.Map<AccountDTO>(dto);
            var entityChanged = _context.Update(account).Entity;
            _context.SaveChanges();
            return entityChanged;
        }

        public override bool Delete(long id)
        {
            var account = _context.Accounts.FirstOrDefault(x => x.Id == id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                var indexChanged = _context.SaveChanges();
                return (indexChanged > 0);
            }
            return false;
        }
    }
}
