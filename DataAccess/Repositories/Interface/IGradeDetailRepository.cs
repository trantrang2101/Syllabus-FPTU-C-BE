﻿using BusinessObject.Models;
using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interface
{
    public interface IGradeDetailRepository : IBaseRepository<GradeDetail, GradeDetailDTO>
    {
        bool UpdateAll(List<GradeDetail> gradeDetailList);
        IEnumerable<GradeDetailDTO> ExportExcel(int courseId);
    }
}
