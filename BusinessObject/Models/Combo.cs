using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Combo : BasicModel
    {
        public Combo()
        {
            ComboCurricula = new HashSet<ComboCurriculum>();
            ComboDetails = new HashSet<ComboDetail>();
        }

        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ComboCurriculum> ComboCurricula { get; set; }
        public virtual ICollection<ComboDetail> ComboDetails { get; set; }
    }
}
