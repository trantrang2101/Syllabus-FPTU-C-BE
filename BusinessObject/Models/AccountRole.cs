using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class AccountRole : BasicModel
    {
        public long? AccountId { get; set; }
        public long? RoleId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Role? Role { get; set; }
    }
}
