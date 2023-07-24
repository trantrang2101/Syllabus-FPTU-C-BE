using BusinessObject.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class StudentCourseDTO : BasicModel
    {
        public string? Note { get; set; }

        public virtual CourseDTO? Course { get; set; }
        public virtual AccountDTO? Student { get; set; }
    }
}
