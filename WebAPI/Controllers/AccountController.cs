﻿using BusinessObject.Models;
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
        [Consumes("text/csv")]
        [Authorize(Roles = "TEACHER")]
        public IActionResult Import(List<AccountDTO> list)
        {
            try
            {
                return Ok(_repository.AddListStudent(list));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse<string>().errWithData(ex.Message).ToJson());
            }
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
        [HttpGet]
        [Authorize(Roles = "TEACHER")]
        public IActionResult Password(long id, string password)
        {
            try
            {
                return Ok(new BaseResponse<object>().successWithData(_repository.CheckPassword(id,password)).ToJson());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse<string>().errWithData(ex.Message).ToJson());
            }
        }
    }
}
