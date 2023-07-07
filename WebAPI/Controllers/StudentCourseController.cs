using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class StudentCourseController : BasicController<StudentCourse, StudentCourseDTO>
    {
        private IStudentCourseRepository _repository;

        public StudentCourseController(IStudentCourseRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
