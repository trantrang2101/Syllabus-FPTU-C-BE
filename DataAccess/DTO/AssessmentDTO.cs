﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public partial class AssessmentDTO : BasicModel
    {
        public string? Name { get; set; }

        public virtual CategoryDTO? Category { get; set; }
    }
}
