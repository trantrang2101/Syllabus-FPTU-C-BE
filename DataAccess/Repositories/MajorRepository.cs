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
    public class MajorRepository : BaseRespository<Major, MajorDTO>, IMajorRepository
    {
        public MajorRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }

        public override MajorDTO Get(long id)
        {
            Major majors = table.Include(x => x.Parent).FirstOrDefault();
            return _mapper.Map<MajorDTO>(majors);
        }

        public override List<MajorDTO> GetAll()
        {
            List<Major> majors = _context.Majors.Include(x => x.Parent).ToList();
            return _mapper.Map<List<MajorDTO>>(majors);
        }
    }
}
