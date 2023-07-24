using BusinessObject.Models;
using DataAccess.DTO;
using DataAccess.DTO;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    public class GradeDetailController : BasicController<GradeDetail, GradeDetailDTO>
    {
        private IGradeDetailRepository _repository;

        public GradeDetailController(IGradeDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Authorize(Roles = "TEACHER")]
        public IActionResult UpdateAll(List<GradeDetail> list)
        {
            try
            {
                return Ok(new BaseResponse<object>().successWithData(_repository.UpdateAll(list)).ToJson());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse<string>().errWithData(ex.Message).ToJson());
            }
        }
    }
}
