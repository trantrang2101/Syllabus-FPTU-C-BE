using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Sidebar : BasicModel
    {
        public Sidebar()
        {
            InverseParent = new HashSet<Sidebar>();
            RoleSidebars = new HashSet<RoleSidebar>();
        }

        public string? Icon { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public long? ParentId { get; set; }
        public string? Url { get; set; }

        public virtual Sidebar? Parent { get; set; }
        public virtual ICollection<Sidebar> InverseParent { get; set; }
        public virtual ICollection<RoleSidebar> RoleSidebars { get; set; }
    }
}
