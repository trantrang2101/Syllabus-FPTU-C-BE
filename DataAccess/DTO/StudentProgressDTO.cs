using BusinessObject.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class StudentProgressDTO : BasicModel
    {
        public int CurriculumStatus { get; set; }
        public int Semester { get; set; }
        public int TermStatus { get; set; }
        public long? CurriculumId { get; set; }
        public long? StudentId { get; set; }
        public long? TermId { get; set; }

        public virtual CurriculumDTO? Curriculum { get; set; }
        public virtual AccountDTO? Student { get; set; }
        public virtual TermDTO? Term { get; set; }
    }
}
