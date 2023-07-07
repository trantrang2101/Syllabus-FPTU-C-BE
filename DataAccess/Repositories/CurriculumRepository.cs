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
    public class CurriculumRepository : BaseRespository<Curriculum, CurriculumDTO>, ICurriculumRepository
    {
        private static ICurriculumRepository _curriculumRepository;
        public CurriculumRepository(IMapper mapper, DatabaseContext context, ICurriculumRepository curriculumRepository) : base(mapper, context)
        {
            _curriculumRepository = curriculumRepository;
        }
    }
}
