using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class CurriculumDetail : BasicModel
    {
        public CurriculumDetail()
        {
            GradeGenerals = new HashSet<GradeGeneral>();
        }

        public int Semester { get; set; }
        public long? CurriculumId { get; set; }
        public long? SubjectId { get; set; }
        public int MinMark { get; set; }

        public virtual Curriculum? Curriculum { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<GradeGeneral> GradeGenerals { get; set; }
    }
}
