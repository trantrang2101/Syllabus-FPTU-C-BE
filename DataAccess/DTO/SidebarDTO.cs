using BusinessObject.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.DTO
{
    public partial class SidebarDTO : BasicModel
    {
        public string? Icon { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }

        public virtual SidebarDTO? Parent { get; set; }

        public virtual ICollection<RoleSidebar> RoleSidebars { get; set; } = new List<RoleSidebar>();
    }
}
