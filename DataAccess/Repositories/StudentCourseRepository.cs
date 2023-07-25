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
    public class StudentCourseRepository : BaseRespository<StudentCourse, StudentCourseDTO>, IStudentCourseRepository
    {
        public StudentCourseRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }

        public override List<StudentCourseDTO> GetAll()
        {
            List<StudentCourse> products = _context.StudentCourses.Include(x=>x.Student).Include("Course.Term").Include("Course.Class").Include("Course.Subject").ToList();
            return _mapper.Map<List<StudentCourseDTO>>(products);
        }

        public override StudentCourseDTO Add(StudentCourseDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Chưa truyền giá trị vào");
            }
            StudentCourse basic = _mapper.Map<StudentCourse>(dto);
            //List<GradeGeneral> list = _context.GradeGenerals.Where(x=>x.)
            SetObjectsToNull(basic);
            StudentCourse saveBasic = table.Add(basic).Entity;
            _context.SaveChanges();
            return Get(saveBasic.Id);
        }
    }
}
