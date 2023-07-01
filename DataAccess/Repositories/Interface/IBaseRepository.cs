using BusinessObject.Models;

namespace DataAccess.Repositories.Interface
{
    public interface IBaseRepository<B,D> where B : BasicModel where D : BasicModel
    {
        D Get(long id);
        List<D> GetAll();
        D Add(D entity);
        D Update(D entity);
        bool Delete(long id);
    }
}
