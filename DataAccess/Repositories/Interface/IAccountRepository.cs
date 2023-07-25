using BusinessObject.Models;
using DataAccess.DTO;

namespace DataAccess.Repositories.Interface
{
    public interface IAccountRepository : IBaseRepository<Account,AccountDTO>
    {
        AccountDTO Login(string username);

        bool CheckPassword(long id, string password);

        bool AddListStudent(List<AccountDTO> accounts);
    }
}
