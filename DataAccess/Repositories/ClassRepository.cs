using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ClassRepository : BaseRespository<Class, ClassDTO>, IClassRepository
    {
        private static IClassRepository _classRepository;

        public ClassRepository(IMapper mapper, DatabaseContext context, IClassRepository classRepository) : base(mapper, context)
        {
            _classRepository = classRepository;
        }
    }
}
