using BusinessObject.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.DTO
{
    public partial class ClassDTO : BasicModel
    {
        public ClassDTO()
        {
            Courses = new HashSet<CourseDTO>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<CourseDTO> Courses { get; set; }
    }
}
