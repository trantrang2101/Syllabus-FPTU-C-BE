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
        public CurriculumDTO()
        {
            ComboCurricula = new HashSet<ComboCurriculumDTO>();
            CurriculumDetails = new HashSet<CurriculumDetailDTO>();
            StudentProgresses = new HashSet<StudentProgressDTO>();
        }

        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public long? MajorId { get; set; }

        public virtual Major? Major { get; set; }
        public virtual ICollection<ComboCurriculumDTO> ComboCurricula { get; set; }
        public virtual ICollection<CurriculumDetailDTO> CurriculumDetails { get; set; }
        public virtual ICollection<StudentProgressDTO> StudentProgresses { get; set; }
    }
}
