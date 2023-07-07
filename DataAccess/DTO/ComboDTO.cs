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
        public ComboDTO()
        {
            ComboCurricula = new HashSet<ComboCurriculumDTO>();
            ComboDetails = new HashSet<ComboDetailDTO>();
        }

        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<ComboCurriculumDTO> ComboCurricula { get; set; }
        public virtual ICollection<ComboDetailDTO> ComboDetails { get; set; }
    }
}
