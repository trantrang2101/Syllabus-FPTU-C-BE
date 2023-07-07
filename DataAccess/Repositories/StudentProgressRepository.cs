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
    public class StudentProgressRepository : BaseRespository<StudentProgress, StudentProgressDTO>, IStudentProgressRepository
    {
        private static IStudentProgressRepository _studentProgressRepository;
        public StudentProgressRepository(IMapper mapper, DatabaseContext context, IStudentProgressRepository studentProgressRepository) : base(mapper, context)
        {
            _studentProgressRepository = studentProgressRepository;
        }
    }
}
