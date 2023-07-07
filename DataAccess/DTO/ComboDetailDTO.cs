using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class ComboDetailDTO : BasicModel
    {
        public string? Description { get; set; }
        public long? ComboId { get; set; }
        public long? SubjectId { get; set; }
        public long? ReplaceSubjectId { get; set; }

        public virtual ComboDTO? ComboDTO { get; set; }
        public virtual SubjectDTO? ReplaceSubject { get; set; }
        public virtual SubjectDTO? Subject { get; set; }
    }
}
