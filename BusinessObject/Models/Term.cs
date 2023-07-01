using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Term : BasicModel
    {
        public Term()
        {
            Courses = new HashSet<Course>();
            StudentProgresses = new HashSet<StudentProgress>();
        }

        public DateTime? EndDate { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<StudentProgress> StudentProgresses { get; set; }
    }
}
