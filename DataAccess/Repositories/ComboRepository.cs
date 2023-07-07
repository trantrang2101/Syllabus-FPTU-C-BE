using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ComboRepository : BaseRespository<Combo, ComboDTO>, IComboRepository
    {
        private static IComboRepository _comboRepository;
        public ComboRepository(IMapper mapper, DatabaseContext context, IComboRepository comboRepository) : base(mapper, context)
        {
            _comboRepository = comboRepository;
        }
    }
}
