using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Role : BasicModel
    {
        public Role()
        {
            AccountRoles = new HashSet<AccountRole>();
            RoleSidebars = new HashSet<RoleSidebar>();
        }

        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<AccountRole> AccountRoles { get; set; }
        public virtual ICollection<RoleSidebar> RoleSidebars { get; set; }
    }
}
