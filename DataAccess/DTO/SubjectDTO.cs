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
        public SubjectDTO()
        {
            ComboDetailReplaceSubjects = new HashSet<ComboDetailDTO>();
            ComboDetailSubjects = new HashSet<ComboDetailDTO>();
            Courses = new HashSet<CourseDTO>();
            CurriculumDetails = new HashSet<CurriculumDetailDTO>();
        }

        public string? Code { get; set; }
        public int Credit { get; set; }
        public string? Name { get; set; }
        public int Slot { get; set; }
        public long? DepartmentId { get; set; }

        public virtual DepartmentDTO? Department { get; set; }
        public virtual ICollection<ComboDetailDTO> ComboDetailReplaceSubjects { get; set; }
        public virtual ICollection<ComboDetailDTO> ComboDetailSubjects { get; set; }
        public virtual ICollection<CourseDTO> Courses { get; set; }
        public virtual ICollection<CurriculumDetailDTO> CurriculumDetails { get; set; }
    }
}
