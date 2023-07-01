using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Class : BasicModel
    {
        public Class()
        {
            Courses = new HashSet<Course>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
