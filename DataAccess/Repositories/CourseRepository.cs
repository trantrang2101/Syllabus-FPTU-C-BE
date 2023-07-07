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
    public class CourseRepository : BaseRespository<Course, CourseDTO>, ICourseRepository
    {
        private static ICourseRepository _courseRepository;
        public CourseRepository(IMapper mapper, DatabaseContext context, ICourseRepository courseRepository) : base(mapper, context)
        {
            _courseRepository = courseRepository;
        }
    }
}
