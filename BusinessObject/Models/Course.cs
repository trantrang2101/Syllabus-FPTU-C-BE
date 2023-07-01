using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Course : BasicModel
    {
        public Course()
        {
            StudentCourses = new HashSet<StudentCourse>();
        }

        public long? ClassId { get; set; }
        public long? SubjectId { get; set; }
        public long? TeacherId { get; set; }
        public long? TermId { get; set; }

        public virtual Class? Class { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual Account? Teacher { get; set; }
        public virtual Term? Term { get; set; }
        public virtual ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
