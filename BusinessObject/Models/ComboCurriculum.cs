using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ComboCurriculum : BasicModel
    {
        public string? Description { get; set; }
        public int TermStatus { get; set; }
        public long? CurriculumId { get; set; }
        public long? ComboId { get; set; }

        public virtual Combo? Combo { get; set; }
        public virtual Curriculum? Curriculum { get; set; }
    }
}
