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
    public class CurriculumDetailRepository : BaseRespository<CurriculumDetail, CurriculumDetailDTO>, ICurriculumDetailRepository
    {
        private static ICurriculumDetailRepository _curriculumDetailRepository;
        public CurriculumDetailRepository(IMapper mapper, DatabaseContext context, ICurriculumDetailRepository curriculumDetailRepository) : base(mapper, context)
        {
            _curriculumDetailRepository = curriculumDetailRepository;
        }
    }
}
