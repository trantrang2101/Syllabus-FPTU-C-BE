using AutoMapper;
using AutoMapper.Execution;
using BusinessObject.Models;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Reflection;

namespace DataAccess.Repositories
{
    public class BaseRespository<B, D> : IBaseRepository<B, D> where B : BasicModel where D : BasicModel
    { 
        protected DatabaseContext _context = null;
        protected IMapper _mapper;
        protected DbSet<B> table = null;

        public BaseRespository(IMapper mapper, DatabaseContext context)
        {
            _context = context;
            _mapper = mapper;
            table = _context.Set<B>();
        }
        public static void SetObjectsToNull(object obj)
        {
            if (obj == null)
                return;

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string)
                        && property.PropertyType.GetInterface(nameof(IEnumerable)) != null)
                {
                    Console.WriteLine(property.Name);
                    // Set properties of class types to null
                    object value = property.GetValue(obj);
                    SetObjectsToNull(value);
                    property.SetValue(obj, null);
                }
            }
        }

        public virtual D Add(D dto)
        {
            if (dto == null)
            {
                throw new Exception("Chưa truyền giá trị vào");
            }
            B basic = _mapper.Map<B>(dto);
            SetObjectsToNull(basic);
            B saveBasic = table.Add(basic).Entity;
            _context.SaveChanges();
            return Get(saveBasic.Id);
        }

        public virtual bool Delete(long id)
        {
            B basic = table.FirstOrDefault(x => x.Id == id);
            if (basic == null || basic.Status == 0)
            {
                throw new Exception("Không tìm thấy hoặc đã bị xóa");
            }
            basic.Status = 0;
            table.Update(basic);
            if(_context.SaveChanges() > 0)
            {
                return true;
            }
            throw new Exception("Không xóa được");
        }

        public virtual D Get(long id)
        {
            B basic = table.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<D>(basic);
        }

        public virtual List<D> GetAll()
        {
            List<B> products = table.ToList();
            return _mapper.Map<List<D>>(products);
        }

        public virtual D Update(D dto)
        {
            if(dto == null)
            {
                throw new Exception("Chưa truyền giá trị vào");
            }
            B basic = table.FirstOrDefault(x => x.Id == dto.Id);
            if (basic == null)
            {
                throw new Exception("Không tìm thấy");
            }
            basic = _mapper.Map<B>(dto);
            SetObjectsToNull(basic);
            B attachedEntity = table.Find(dto.Id);
            if (attachedEntity != null)
            {
                var attachedEntry = _context.Entry(attachedEntity);
                attachedEntry.CurrentValues.SetValues(basic);
            }
            else
            {
                _context.Entry(basic).State = EntityState.Modified;
            }
            var changes = _context.SaveChanges();
            Console.WriteLine(changes);
            return Get(dto.Id);
        }
    }
}
