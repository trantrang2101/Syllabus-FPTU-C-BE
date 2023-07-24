using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class GradeDetailRepository : BaseRespository<GradeDetail, GradeDetailDTO>, IGradeDetailRepository
    {
        public GradeDetailRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }

        public override List<GradeDetailDTO> GetAll()
        {
            List<GradeDetail> products = _context.GradeDetails.Include("StudentCourse.Course").Include("StudentCourse.Student").Include("GradeGeneral.Assessment.Category").ToList();
            List<GradeDetailDTO> dto = new List<GradeDetailDTO>() { };
            if (products != null && products.Count() > 0)
            {
                return _mapper.Map<List<GradeDetailDTO>>(products);
            }
            return dto;
        }

        public bool UpdateAll(List<GradeDetail> gradeDetailList)
        {
            _context.GradeDetails.UpdateRange(gradeDetailList);
            var changes = _context.SaveChanges();
            if (changes>0)
            {
                return true;
            }
            throw new Exception("Không có gì thay đổi");
        }
    }
}
