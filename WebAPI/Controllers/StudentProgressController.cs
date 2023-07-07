using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class StudentProgressController : BasicController<StudentProgress, StudentProgressDTO>
    {
        private IStudentProgressRepository _repository;

        public StudentProgressController(IStudentProgressRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
