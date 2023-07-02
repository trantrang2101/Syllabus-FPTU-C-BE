using DataAccess.Models;

namespace DataAccess.Repositories.Interface
{
    public interface IAuthenticationRepository
    {
        string GetJwtToken(AccountDTO user);
    }
}
