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
    public class AssessmentRepository : BaseResponsitory<Assessment, AssessmentDTO>, IAssessmentRepository
    {
        private static IAssessmentRepository _assessmentRepository;
        public AssessmentRepository(IMapper mapper, DatabaseContext context, IAssessmentRepository assessmentRepository) : base(mapper, context)
        {
            _assessmentRepository = assessmentRepository;
        }
    }
}
