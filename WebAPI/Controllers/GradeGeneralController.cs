using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class GradeGeneralController : BasicController<GradeGeneral, GradeGeneralDTO>
    {
        private IGradeGeneralRepository _repository;

        public GradeGeneralController(IGradeGeneralRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
