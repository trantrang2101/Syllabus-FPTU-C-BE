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
    public class SubjectRepository : BaseRespository<Subject, SubjectDTO>, ISubjectRepository
    {
        public SubjectRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }

        public override List<SubjectDTO> GetAll()
        {
            List<Subject> products = _context.Subjects.Include(x => x.Department).ToList();
            return _mapper.Map<List<SubjectDTO>>(products);
        }
    }
}
