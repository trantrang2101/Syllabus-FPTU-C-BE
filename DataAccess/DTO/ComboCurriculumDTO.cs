using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class ComboCurriculumDTO : BasicModel
    {
        public string? Description { get; set; }
        public int TermStatus { get; set; }
        public long? CurriculumId { get; set; }

        public virtual ComboDTO? Combo { get; set; }
        public virtual CurriculumDTO? Curriculum { get; set; }
    }
}
