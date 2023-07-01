using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class ComboDetail : BasicModel
    {
        public string? Description { get; set; }
        public long? ComboId { get; set; }
        public long? SubjectId { get; set; }
        public long? ReplaceSubjectId { get; set; }

        public virtual Combo? Combo { get; set; }
        public virtual Subject? ReplaceSubject { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}
