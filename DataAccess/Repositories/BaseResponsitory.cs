using AutoMapper;
using BusinessObject.Models;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class BaseResponsitory<B, D> : IBaseRepository<B, D> where B : BasicModel where D : BasicModel
    { 
        protected DatabaseContext _context = null;
        protected IMapper _mapper;
        protected DbSet<B> table = null;

        public BaseResponsitory(IMapper mapper, DatabaseContext context)
        {
            _context = context;
            _mapper = mapper;
            table = _context.Set<B>();
        }


        public virtual D Add(D dto)
        {
            if (dto != null)
            {
                return null;
            }
            B basic = _mapper.Map<B>(dto);
            B saveBasic = table.Add(basic).Entity;
            _context.SaveChanges();
            return Get(saveBasic.Id);
        }

        public virtual bool Delete(long id)
        {
            B basic = table.FirstOrDefault(x => x.Id == id);
            if (basic == null || basic.Status == 0)
            {
                throw new Exception("Cannot Found " + nameof(B));
            }
            basic.Status = 0;
            table.Update(basic);
            return _context.SaveChanges() > 0;
        }

        public virtual D Get(long id)
        {
            return _mapper.Map<D>(table.FirstOrDefault(x => x.Id == id));
        }

        public virtual List<D> GetAll()
        {
            List<B> products = table.ToList();
            return _mapper.Map<List<D>>(products);
        }

        public virtual D Update(D dto)
        {
            B basic = _mapper.Map<B>(dto);
            table.Attach(basic);
            _context.Entry(basic).State = EntityState.Modified;
            _context.SaveChanges();
            return dto;
        }
    }
}
