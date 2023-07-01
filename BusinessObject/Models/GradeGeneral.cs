using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class GradeGeneral : BasicModel
    {
        public GradeGeneral()
        {
            GradeDetails = new HashSet<GradeDetail>();
        }

        public double? Weight { get; set; }
        public long? AssessmentId { get; set; }
        public long? CurriculumDetailId { get; set; }
        public int MinMark { get; set; }

        public virtual Assessment? Assessment { get; set; }
        public virtual CurriculumDetail? CurriculumDetail { get; set; }
        public virtual ICollection<GradeDetail> GradeDetails { get; set; }
    }
}
