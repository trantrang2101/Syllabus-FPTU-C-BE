using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class CourseController : BasicController<Course,CourseDTO>
    {
        private ICourseRepository _repository;

        public CourseController(ICourseRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
