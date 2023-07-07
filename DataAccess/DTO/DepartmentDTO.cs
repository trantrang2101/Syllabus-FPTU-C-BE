using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class DepartmentDTO : BasicModel
    {
        public DepartmentDTO()
        {
            Subjects = new HashSet<SubjectDTO>();
        }

        public string? Name { get; set; }

        public virtual ICollection<SubjectDTO> Subjects { get; set; }
    }
}
