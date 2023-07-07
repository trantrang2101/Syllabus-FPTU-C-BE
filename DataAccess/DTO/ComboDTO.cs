using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class ComboDTO : BasicModel
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ComboCurriculumDTO> ComboCurricula { get; set; } = new HashSet<ComboCurriculumDTO>();
        public virtual ICollection<ComboDetailDTO> ComboDetails { get; set; } = new HashSet<ComboDetailDTO>();
    }
}
