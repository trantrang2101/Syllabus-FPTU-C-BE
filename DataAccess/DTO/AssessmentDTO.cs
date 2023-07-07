using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class AssessmentDTO : BasicModel
    {
        public AssessmentDTO()
        {
            GradeGenerals = new HashSet<GradeGeneral>();
        }

        public string? Name { get; set; }
        public long? CategoryId { get; set; }

        public virtual CategoryDTO? Category { get; set; }
        public virtual ICollection<GradeGeneral> GradeGenerals { get; set; }
    }
}
