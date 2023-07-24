using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CurriculumDetailRepository : BaseRespository<CurriculumDetail, CurriculumDetailDTO>, ICurriculumDetailRepository
    {
        public CurriculumDetailRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }

        public override CurriculumDetailDTO Get(long id)
        {
            CurriculumDetail basic = _context.CurriculumDetails.Include(x => x.Subject).Include(x => x.Curriculum).FirstOrDefault(x => x.Id == id);
            return _mapper.Map<CurriculumDetailDTO>(basic);
        }
    }
}
