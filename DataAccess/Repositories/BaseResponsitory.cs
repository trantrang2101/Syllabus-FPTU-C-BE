using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.Repositories.Interface;

namespace DataAccess.Repositories
{
    internal class BaseResponsitory<B, D> : IBaseRepository<B, D> where B : BasicModel where D : BasicModel
    {

        private static BaseDAO<B,D> dao;

        public BaseResponsitory(IMapper mapper)
        {
            dao = BaseDAO<B, D>.getInstance(mapper);
        }

        public D Add(D entity)
        {
            return dao.Add(entity);
        }

        public bool Delete(long id)
        {
            return dao.Delete(id);
        }

        public D Get(long id)
        {
            return dao.GetById(id);
        }

        public List<D> GetAll()
        {
            return dao.GetAll();
        }

        public D Update(D entity)
        {
            return dao.Update(entity);
        }
    }
}
