using System;
using System.Collections.Generic;

namespace BusinessObject.Models

{
    public partial class Curriculum : BasicModel
    {
        public Curriculum()
        {
            CurriculumDetails = new HashSet<CurriculumDetail>();
            StudentProgresses = new HashSet<StudentProgress>();
        }

        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public long? MajorId { get; set; }

        public virtual Major? Major { get; set; }
        public virtual ICollection<CurriculumDetail> CurriculumDetails { get; set; }
        public virtual ICollection<StudentProgress> StudentProgresses { get; set; }
    }
}
