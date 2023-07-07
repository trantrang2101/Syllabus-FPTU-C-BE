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
    public class DepartmentRepository : BaseRespository<Department, DepartmentDTO>, IDepartmentRepository
    {
        private static IDepartmentRepository _departmentRepository;
        public DepartmentRepository(IMapper mapper, DatabaseContext context, IDepartmentRepository departmentRepository) : base(mapper, context)
        {
            _departmentRepository = departmentRepository;
        }
    }
}
