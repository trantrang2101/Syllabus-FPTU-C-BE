using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class TermDTO : BasicModel
    {
        public DateTime? EndDate { get; set; }
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }

        public virtual ICollection<CourseDTO> Courses { get; set; } = new HashSet<CourseDTO>();
        public virtual ICollection<StudentProgressDTO> StudentProgresses { get; set; } = new HashSet<StudentProgressDTO>();
    }
}
