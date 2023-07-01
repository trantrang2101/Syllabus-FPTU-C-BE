using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Department : BasicModel
    {
        public Department()
        {
            Subjects = new HashSet<Subject>();
        }

        public string? Name { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
