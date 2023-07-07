using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class CurriculumDetailController : BasicController<CurriculumDetail, CurriculumDetailDTO>
    {
        private ICurriculumDetailRepository _repository;

        public CurriculumDetailController(ICurriculumDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
