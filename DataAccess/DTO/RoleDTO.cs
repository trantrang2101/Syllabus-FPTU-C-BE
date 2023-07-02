﻿using BusinessObject.Models;

namespace DataAccess.Models
{
    public partial class RoleDTO : BasicModel
    {
        public RoleDTO()
        {
            AccountRoles = new HashSet<AccountRoleDTO>();
            RoleSidebars = new HashSet<SidebarDTO>();
        }

        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<AccountRoleDTO> AccountRoles { get; set; }
        public virtual ICollection<SidebarDTO> RoleSidebars { get; set; }
    }
}
