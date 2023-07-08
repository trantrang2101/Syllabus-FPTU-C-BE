using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;

namespace DataAccess.Repositories
{
    public class AssessmentRepository : BaseRespository<Assessment, AssessmentDTO>, IAssessmentRepository
    {
        public AssessmentRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }
    }
}
