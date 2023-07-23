using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Subject : BasicModel
    {
        public Subject()
        {
            ComboDetailReplaceSubjects = new HashSet<ComboDetail>();
            ComboDetailSubjects = new HashSet<ComboDetail>();
            Courses = new HashSet<Course>();
            CurriculumDetails = new HashSet<CurriculumDetail>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public int Slot { get; set; }
        public long? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
        public virtual ICollection<ComboDetail> ComboDetailReplaceSubjects { get; set; }
        public virtual ICollection<ComboDetail> ComboDetailSubjects { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<CurriculumDetail> CurriculumDetails { get; set; }
    }
}
