using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class SubjectController : BasicController<Subject, SubjectDTO>
    {
        private ISubjectRepository _repository;

        public SubjectController(ISubjectRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
