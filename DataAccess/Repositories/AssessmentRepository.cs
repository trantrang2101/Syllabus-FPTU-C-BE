using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class AssessmentRepository : BaseRespository<Assessment, AssessmentDTO>, IAssessmentRepository
    {
        public AssessmentRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }

        public override List<AssessmentDTO> GetAll()
        {
            List<Assessment> assessments = _context.Assessments.Include(x => x.Category).ToList();
            return _mapper.Map<List<AssessmentDTO>>(assessments);
        }

        public override AssessmentDTO Get(long id)
        {
            Assessment basic = table.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            return _mapper.Map<AssessmentDTO>(basic);
        }
    }
}
