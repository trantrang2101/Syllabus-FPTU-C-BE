using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Assessment : BasicModel
    {
        public Assessment()
        {
            GradeGenerals = new HashSet<GradeGeneral>();
        }

        public string? Name { get; set; }
        public long? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<GradeGeneral> GradeGenerals { get; set; }
    }
}
