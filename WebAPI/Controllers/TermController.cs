﻿using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class TermController : BasicController<Term, TermDTO>
    {
        private ITermRepository _repository;

        public TermController(ITermRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
