using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class GradeDetailController : BasicController<GradeDetail, GradeDetailDTO>
    {
        private IGradeDetailRepository _repository;

        public GradeDetailController(IGradeDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
