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
    public class MajorRepository : BaseRespository<Major, MajorDTO>, IMajorRepository
    {
        private static IMajorRepository _majorRepository;
        public MajorRepository(IMapper mapper, DatabaseContext context, IMajorRepository majorRepository) : base(mapper, context)
        {
            _majorRepository = majorRepository;
        }
    }
}
