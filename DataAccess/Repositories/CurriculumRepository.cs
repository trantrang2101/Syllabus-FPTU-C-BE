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
    public class CurriculumRepository : BaseRespository<Curriculum, CurriculumDTO>, ICurriculumRepository
    {
        public CurriculumRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }

        public override List<CurriculumDTO> GetAll()
        {
            List<Curriculum> products = _context.Curricula.Include(x=>x.Major).ToList();
            return _mapper.Map<List<CurriculumDTO>>(products);
        }

        public override CurriculumDTO Get(long id)
        {
            Curriculum basic = table.Include(x => x.Major).Include("ComboCurricula.Combo").Include("CurriculumDetails.Subject").FirstOrDefault(x => x.Id == id);
            return _mapper.Map<CurriculumDTO>(basic);
        }
    }
}
