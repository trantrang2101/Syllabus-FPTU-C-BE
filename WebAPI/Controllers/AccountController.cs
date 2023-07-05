using BusinessObject.Models;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class AccountController : BasicController<Account,AccountDTO>
    {
        private IAccountReponsitory _repository;
        public AccountController(IAccountReponsitory repository) : base(repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Login(string email)
        {
            try
            {
                return Ok(new BaseResponse<string>().successWithData(_repository.Login(email)).ToJson());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse<string>().errWithData(ex.Message).ToJson());
            }
        }
    }
}
