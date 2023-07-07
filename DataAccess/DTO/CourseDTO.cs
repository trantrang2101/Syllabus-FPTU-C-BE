using BusinessObject.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class CourseDTO : BasicModel
    {
        public virtual ClassDTO? Class { get; set; }
        public virtual SubjectDTO? Subject { get; set; }
        public virtual AccountDTO? Teacher { get; set; }
        public virtual TermDTO? Term { get; set; }
        public virtual ICollection<StudentCourseDTO> StudentCourses { get; set; } = new HashSet<StudentCourseDTO>();
    }
}
