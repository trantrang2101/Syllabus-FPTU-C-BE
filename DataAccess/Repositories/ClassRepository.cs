using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;

namespace DataAccess.Repositories
{
    public class ClassRepository : BaseRespository<Class, ClassDTO>, IClassRepository
    {
        public ClassRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }
}
