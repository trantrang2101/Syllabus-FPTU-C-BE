using AutoMapper;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    internal class BaseDAO<B,D> where B:BasicModel where D:BasicModel
    {
        private static BaseDAO<B, D> instance;
        private DatabaseContext _context = null;
        private static IMapper _mapper;
        private DbSet<B> table = null;
        internal BaseDAO(IMapper mapper, DatabaseContext context)
        {
            _context = context;
            table = _context.Set<B>();
        }

        internal static BaseDAO<B, D> getInstance(IMapper mapper)
        {
            instance ??= new BaseDAO<B, D>(mapper, new DatabaseContext());
            return instance;
        }

        internal List<D> GetAll()
        {
            List<B> products = table.ToList();
            List<D> dto = new List<D>() { };
            if (products != null && products.Count() > 0)
            {
                return _mapper.Map<List<D>>(products);
            }
            return dto;
        }

        internal D GetById(long id) {
            return _mapper.Map<D>(table.FirstOrDefault(x => x.Id == id));
        }
        internal D Add(D dto)
        {
            B basic = _mapper.Map<B>(dto);
            B saveBasic = table.Add(basic).Entity;
            _context.SaveChanges();
            return GetById(saveBasic.Id);
        }

        internal D Update(D dto)
        {
            B basic = _mapper.Map<B>(dto);
            table.Attach(basic);
            _context.Entry(basic).State = EntityState.Modified;
            return dto;
        }

        internal bool Delete(long id)
        {
            B basic = table.FirstOrDefault(x => x.Id == id);
            if(basic == null || basic.Status == 0) { 
                return false;
            }
            basic.Status = 0;
            table.Update(basic);
            return _context.SaveChanges()>0;
        }
    }
}
