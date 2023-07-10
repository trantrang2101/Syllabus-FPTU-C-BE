using BusinessObject.Models;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BasicController<B,D> : Controller where B:BasicModel where D:BasicModel
    {
        private readonly IBaseRepository<B,D> _repository;

        public BasicController(IBaseRepository<B, D> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        [EnableQuery]
        [Authorize]
        public virtual IActionResult List()
        {
            try
            {
                List<D> products = _repository.GetAll();
                if (products == null)
                {
                    throw new Exception("Không tìm thấy danh sách thích hợp");
                }

                return Ok(new BaseResponse<List<D>>().successWithData(products).ToJson());
            }
            catch(Exception ex)
            {
                return Ok(new BaseResponse<string>().errWithData(ex.Message).ToJson());
            }
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "head")]
        public virtual IActionResult Detail(int id)
        {
            try
            {
                D dto = _repository.Get(id);
                if (dto == null)
                {
                    throw new Exception("Không tìm thấy hoặc đã bị xóa");
                }
                return Ok(new BaseResponse<D>().successWithData(dto).ToJson());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse<string>().errWithData(ex.Message).ToJson());
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public virtual IActionResult Delete(int id)
        {
            try
            {
                D dto = _repository.Get(id);
                _repository.Delete(id);
                return Ok(new BaseResponse<D>().successWithData(dto).ToJson());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse<string>().errWithData(ex.Message).ToJson());
            }
        }
        [HttpPost]
        [Authorize]
        public virtual IActionResult Add(D dto)
        {
            try
            {
                D p = _repository.Add(dto);
                return Ok(new BaseResponse<D>().successWithData(p).ToJson());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse<string>().errWithData(ex.Message).ToJson());
            }
        }
        [HttpPost]
        [Authorize]
        public virtual IActionResult Update(D dto)
        {
            try
            {
                D p = _repository.Update(dto);
                return Ok(new BaseResponse<D>().successWithData(p).ToJson());
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse<string>().errWithData(ex.Message).ToJson());
            }
        }
    }
}
