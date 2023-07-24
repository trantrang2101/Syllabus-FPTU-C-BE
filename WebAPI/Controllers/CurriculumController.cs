using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class CurriculumController : BasicController<Curriculum,CurriculumDTO>
    {
        private ICurriculumRepository _repository;

        public CurriculumController(ICurriculumRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
