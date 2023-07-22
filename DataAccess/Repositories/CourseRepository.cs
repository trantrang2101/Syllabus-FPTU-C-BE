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
    public class CourseRepository : BaseRespository<Course, CourseDTO>, ICourseRepository
    {
        public CourseRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
        public override List<CourseDTO> GetAll()
        {
            List<Course> courses = _context.Courses.Include(x => x.Class).Include(x => x.Subject).Include(x => x.Teacher).Include(x => x.Term).ToList();
            return _mapper.Map<List<CourseDTO>>(courses);
        }

        public override CourseDTO Get(long id)
        {
            Course basic = table.Include(x => x.Class).Include(x => x.Subject).Include(x => x.Teacher).Include(x => x.Term).FirstOrDefault(x => x.Id == id);
            return _mapper.Map<CourseDTO>(basic);
        }
    }
}
