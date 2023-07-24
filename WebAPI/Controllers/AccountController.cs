using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class AccountController : BasicController<Account,AccountDTO>
    {
        private IAccountRepository _repository;
        public AccountController(IAccountRepository repository) : base(repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string gmail)
        {
            try
            {
                return Ok(new BaseResponse<object>().successWithData(_repository.Login(gmail)).ToJson());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse<string>().errWithData(ex.Message).ToJson());
            }
        }
    }
}
