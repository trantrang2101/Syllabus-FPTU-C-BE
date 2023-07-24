using BusinessObject.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.DTO
{
    public partial class RoleSidebarDTO : BasicModel
    {
        public virtual RoleDTO? Role { get; set; }
        public virtual SidebarDTO? Sidebar { get; set; }
    }
}
