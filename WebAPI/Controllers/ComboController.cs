using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class ComboController : BasicController<Combo,ComboDTO>
    {
        private IComboRepository _repository;

        public ComboController(IComboRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
