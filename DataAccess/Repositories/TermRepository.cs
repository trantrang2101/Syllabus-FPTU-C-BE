﻿using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class TermRepository : BaseRespository<Term, TermDTO>, ITermRepository
    {
        private static ITermRepository _termRepository;

        public TermRepository(IMapper mapper, DatabaseContext context, ITermRepository termRepository) : base(mapper, context)
        {
            _termRepository = termRepository;
        }
    }
}
