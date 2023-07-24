using BusinessObject.Models;
using Newtonsoft.Json;

namespace DataAccess.DTO
{
    public partial class AccountDTO : BasicModel
    {
        public AccountDTO()
        {
            Roles = new HashSet<RoleDTO>();
            Sidebars = new HashSet<SidebarDTO>();
        }

        public string? Code { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        [JsonIgnore]
        public string? Password { get; set; }
        public string? Token { get; set; }
        public TermDTO? CurrentTerm { get; set; }
        public virtual ICollection<RoleDTO> Roles { get; set; }
        public virtual ICollection<SidebarDTO> Sidebars{ get; set; }
    }
}
