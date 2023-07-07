using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class AssessmentController : BasicController<Assessment, AssessmentDTO>
    {
        private IAssessmentRepository _repository;
        public AssessmentController(IAssessmentRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
