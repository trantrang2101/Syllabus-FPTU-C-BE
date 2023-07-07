using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class CategoryDTO: BasicModel
    {
        public CategoryDTO()
        {
            Assessments = new HashSet<AssessmentDTO>();
        }

        public string? Name { get; set; }

        public virtual ICollection<AssessmentDTO> Assessments { get; set; }
    }
}
