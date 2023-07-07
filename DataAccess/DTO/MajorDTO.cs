using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class MajorDTO : BasicModel
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual MajorDTO? Parent { get; set; }
        public virtual ICollection<CurriculumDTO> Curricula { get; set; } = new HashSet<CurriculumDTO>();
        public virtual ICollection<MajorDTO> InverseParent { get; set; } = new HashSet<MajorDTO>();
    }
}
