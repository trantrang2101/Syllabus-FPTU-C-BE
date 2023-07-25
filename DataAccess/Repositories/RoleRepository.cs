using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RoleRepository : BaseRespository<Role, RoleDTO>, IRoleRepository
    {
        public RoleRepository(IMapper mapper, DatabaseContext context) : base(mapper, context)
        {
        }

        public override RoleDTO Add(RoleDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Chưa truyền giá trị vào");
            }
            Role basic = _mapper.Map<Role>(dto);
            SetObjectsToNull(basic);
            Role saveBasic = table.Add(basic).Entity;
            if (_context.SaveChanges() > 0)
            {
                foreach (var item in dto.Sidebars)
                {
                    _context.RoleSidebars.Add(new RoleSidebar()
                    {
                        RoleId = saveBasic.Id,
                        SidebarId = item.Id,
                    });
                }
                _context.SaveChanges();
            }
            return Get(saveBasic.Id);
        }

        public override List<RoleDTO> GetAll()
        {
            List<Role> products = _context.Roles.Include(x => x.RoleSidebars).ThenInclude(x => x.Sidebar).ToList();
            List<RoleDTO> dto = new List<RoleDTO>() { };
            if (products != null && products.Count() > 0)
            {
                return _mapper.Map<List<RoleDTO>>(products);
            }
            return dto;
        }

        public override RoleDTO Update(RoleDTO dto)
        {
            if (dto == null)
            {
                throw new Exception("Chưa truyền giá trị vào");
            }
            Role basic = table.FirstOrDefault(x => x.Id == dto.Id);
            if (basic == null)
            {
                throw new Exception("Không tìm thấy");
            }
            basic = _mapper.Map<Role>(dto);
            SetObjectsToNull(basic);
            Role attachedEntity = table.Find(dto.Id);
            if (attachedEntity != null)
            {
                var attachedEntry = _context.Entry(attachedEntity);
                attachedEntry.CurrentValues.SetValues(basic);
            }
            else
            {
                _context.Entry(basic).State = EntityState.Modified;
            }
            _context.RoleSidebars.RemoveRange(_context.RoleSidebars.Where(x => x.RoleId == dto.Id));
            foreach (var item in dto.Sidebars)
            {
                _context.RoleSidebars.Add(new RoleSidebar()
                {
                    RoleId = dto.Id,
                    SidebarId = item.Id,
                });
            }
            _context.SaveChanges();
            return Get(dto.Id);
        }
    }
}
