using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class StudentCourse : BasicModel
    {
        public StudentCourse()
        {
            GradeDetails = new HashSet<GradeDetail>();
        }

        public string? Note { get; set; }
        public long? CourseId { get; set; }
        public long? StudentId { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Account? Student { get; set; }
        public virtual ICollection<GradeDetail> GradeDetails { get; set; }
    }
}
