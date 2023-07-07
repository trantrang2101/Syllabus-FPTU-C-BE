using BusinessObject.Models;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interface
{
    public interface ISidebarRepository : IBaseRepository<Sidebar, SidebarDTO>
    {
    }
}
