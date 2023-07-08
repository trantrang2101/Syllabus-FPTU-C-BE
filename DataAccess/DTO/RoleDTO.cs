using BusinessObject.Models;

namespace DataAccess.Models
{
    public partial class RoleDTO : BasicModel
    {
        public string? Code { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<SidebarDTO> Sidebars { get; set; } = new HashSet<SidebarDTO>();
    }
}
