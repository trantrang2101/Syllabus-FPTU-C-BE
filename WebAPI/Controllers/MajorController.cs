using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class MajorController : BasicController<Major, MajorDTO>
    {
        private IMajorRepository _repository;

        public MajorController(IMajorRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
