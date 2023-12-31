﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class GradeGeneralDTO : BasicModel
    {
        public double? Weight { get; set; }
        public int MinMark { get; set; }

        public virtual AssessmentDTO? Assessment { get; set; }
        public virtual CurriculumDetailDTO? CurriculumDetail { get; set; }
    }
}
