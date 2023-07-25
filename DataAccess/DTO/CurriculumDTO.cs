using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class CurriculumDTO : BasicModel
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual MajorDTO? Major { get; set; }
        public virtual ICollection<CurriculumDetailDTO> CurriculumDetails { get; set; } = new HashSet<CurriculumDetailDTO>();
        public virtual ICollection<StudentProgressDTO> StudentProgresses { get; set; } = new HashSet<StudentProgressDTO>();
    }
}
