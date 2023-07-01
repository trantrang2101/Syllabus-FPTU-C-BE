using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class GradeDetail : BasicModel
    {
        public string? Comment { get; set; }
        public DateTime? InsertedDate { get; set; }
        public double? Mark { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? GradeGeneralId { get; set; }
        public long? StudentCourseId { get; set; }

        public virtual GradeGeneral? GradeGeneral { get; set; }
        public virtual StudentCourse? StudentCourse { get; set; }
    }
}
