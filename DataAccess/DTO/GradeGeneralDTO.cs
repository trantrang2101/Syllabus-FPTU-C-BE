using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class GradeGeneralDTO : BasicModel
    {
        public GradeGeneralDTO()
        {
            GradeDetails = new HashSet<GradeDetailDTO>();
        }

        public double? Weight { get; set; }
        public long? AssessmentId { get; set; }
        public long? CurriculumDetailId { get; set; }
        public int MinMark { get; set; }

        public virtual AssessmentDTO? Assessment { get; set; }
        public virtual CurriculumDetailDTO? CurriculumDetail { get; set; }
        public virtual ICollection<GradeDetailDTO> GradeDetails { get; set; }
    }
}
