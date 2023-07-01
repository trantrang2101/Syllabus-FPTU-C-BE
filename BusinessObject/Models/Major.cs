using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Major : BasicModel
    {
        public Major()
        {
            Curricula = new HashSet<Curriculum>();
            InverseParent = new HashSet<Major>();
        }

        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public long? ParentId { get; set; }

        public virtual Major? Parent { get; set; }
        public virtual ICollection<Curriculum> Curricula { get; set; }
        public virtual ICollection<Major> InverseParent { get; set; }
    }
}
