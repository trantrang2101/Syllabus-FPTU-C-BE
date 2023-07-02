using AutoMapper;
using BusinessObject.Models;
using DataAccess.Models;

namespace DataAccess.Ultis
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<AccountRole, AccountRoleDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<RoleSidebar, RoleSidebarDTO>().ReverseMap();
            CreateMap<Sidebar, SidebarDTO>().ReverseMap();
            CreateMap<Account,AccountDTO>().ReverseMap();
        }

        protected internal Mapper(string profileName) : base(profileName) { }
    }
}
