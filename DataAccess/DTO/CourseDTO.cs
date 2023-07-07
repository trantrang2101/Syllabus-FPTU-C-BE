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
        public CourseDTO()
        {
            StudentCourses = new HashSet<StudentCourseDTO>();
        }

        public long? ClassId { get; set; }
        public long? SubjectId { get; set; }
        public long? TeacherId { get; set; }
        public long? TermId { get; set; }

        public virtual ClassDTO? Class { get; set; }
        public virtual SubjectDTO? Subject { get; set; }
        public virtual AccountDTO? Teacher { get; set; }
        public virtual TermDTO? Term { get; set; }
        public virtual ICollection<StudentCourseDTO> StudentCourses { get; set; }
    }
}
