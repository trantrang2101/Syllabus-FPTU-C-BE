using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class StudentProgress : BasicModel
    {
        public int CurriculumStatus { get; set; }
        public int Semester { get; set; }
        public int TermStatus { get; set; }
        public long? CurriculumId { get; set; }
        public long? StudentId { get; set; }
        public long? TermId { get; set; }

        public virtual Curriculum? Curriculum { get; set; }
        public virtual Account? Student { get; set; }
        public virtual Term? Term { get; set; }
    }
}
