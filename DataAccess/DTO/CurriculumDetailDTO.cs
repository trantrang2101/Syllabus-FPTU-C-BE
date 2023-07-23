using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class CurriculumDetailDTO : BasicModel
    {
        public int Semester { get; set; }
        public int MinMark { get; set; }
        public int Credit { get; set; }

        public virtual CurriculumDTO? Curriculum { get; set; }
        public virtual SubjectDTO? Subject { get; set; }
        public virtual ICollection<GradeGeneralDTO> GradeGenerals { get; set; } = new HashSet<GradeGeneralDTO>();
    }
}
