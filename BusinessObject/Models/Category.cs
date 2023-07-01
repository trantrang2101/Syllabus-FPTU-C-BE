using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Category : BasicModel
    {
        public Category()
        {
            Assessments = new HashSet<Assessment>();
        }

        public string? Name { get; set; }

        public virtual ICollection<Assessment> Assessments { get; set; }
    }
}
