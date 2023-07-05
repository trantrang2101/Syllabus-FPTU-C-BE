using BusinessObject.Models;

namespace DataAccess.Models
{
    public partial class AccountDTO : BasicModel
    {
        public AccountDTO()
        {
            Roles = new HashSet<RoleDTO>();
        }

        public string? Code { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<RoleDTO> Roles { get; set; }
    }
}
