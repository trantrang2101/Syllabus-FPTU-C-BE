
using BusinessObject.Models;

namespace DataAccess.DTO
{
    public partial class AccountRoleDTO : BasicModel
    {
        public virtual AccountDTO? Account { get; set; }
        public virtual RoleDTO? Role { get; set; }
    }
}
