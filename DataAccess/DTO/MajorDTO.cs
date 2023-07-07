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
        public MajorDTO()
        {
            Curricula = new HashSet<CurriculumDTO>();
            InverseParent = new HashSet<MajorDTO>();
        }

        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public long? ParentId { get; set; }

        public virtual MajorDTO? Parent { get; set; }
        public virtual ICollection<CurriculumDTO> Curricula { get; set; }
        public virtual ICollection<MajorDTO> InverseParent { get; set; }
    }
}
