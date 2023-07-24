using DataAccess.DTO;

namespace DataAccess.Repositories.Interface
{
    public interface IAuthenticationRepository
    {
        string GetJwtToken(AccountDTO user);
    }
}
