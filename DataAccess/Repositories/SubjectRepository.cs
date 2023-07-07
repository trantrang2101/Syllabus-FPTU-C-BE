using AutoMapper;
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
    public class SubjectRepository : BaseRespository<Subject, SubjectDTO>, ISubjectRepository
    {
        private static ISubjectRepository _subjectRepository;

        public SubjectRepository(IMapper mapper, DatabaseContext context, ISubjectRepository subjectRepository) : base(mapper, context)
        {
            _subjectRepository = subjectRepository;
        }
    }
}
