using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class RoleSidebar : BasicModel
    {
        public long? SidebarId { get; set; }
        public long? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Sidebar? Sidebar { get; set; }
    }
}
