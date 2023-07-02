using BusinessObject.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public partial class AccountDTO : BasicModel
    {
        public AccountDTO()
        {
            AccountRoles = new HashSet<AccountRoleDTO>();
        }

        public string? Code { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public int? Role { get; set; }

        public virtual ICollection<AccountRoleDTO> AccountRoles { get; set; }
    }
}
