using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class GradeDetailDTO : BasicModel
    {
        public string? Comment { get; set; }
        public DateTime? InsertedDate { get; set; }
        public double? Mark { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? GradeGeneralId { get; set; }
        public long? StudentCourseId { get; set; }

        public virtual GradeGeneralDTO? GradeGeneral { get; set; }
        public virtual StudentCourseDTO? StudentCourse { get; set; }
    }
}
