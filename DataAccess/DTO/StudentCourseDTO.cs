using BusinessObject.Models;
using DataAccess.Models;
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
        public virtual ICollection<GradeDetailDTO> GradeDetails { get; set; } = new HashSet<GradeDetailDTO>();
    }
}
