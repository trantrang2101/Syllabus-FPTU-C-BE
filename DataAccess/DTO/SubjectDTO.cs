using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class SubjectDTO : BasicModel
    {
        public string? Code { get; set; }
        public int Credit { get; set; }
        public string? Name { get; set; }
        public int Slot { get; set; }

        public virtual DepartmentDTO? Department { get; set; }
        public virtual ICollection<ComboDetailDTO> ComboDetailReplaceSubjects { get; set; } = new HashSet<ComboDetailDTO>();
        public virtual ICollection<ComboDetailDTO> ComboDetailSubjects { get; set; } = new HashSet<ComboDetailDTO>();
        public virtual ICollection<CourseDTO> Courses { get; set; } = new HashSet<CourseDTO>();
        public virtual ICollection<CurriculumDetailDTO> CurriculumDetails { get; set; } = new HashSet<CurriculumDetailDTO>();
    }
}
