using BusinessObject.Models;
using DataAccess.DTO;

namespace DataAccess.Repositories.Interface
{
    public interface IAccountRepository : IBaseRepository<Account,AccountDTO>
    {
        AccountDTO Login(string username);
    }
}
