using BusinessObject.Models;
using DataAccess.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        [Authorize]
        public virtual IActionResult List(ODataQueryOptions<D> opts)
        {
            try
            {
                List<D> products = _repository.GetAll();
                if (products == null)
                {
                    throw new Exception("Không tìm thấy danh sách thích hợp");
                }
                var query = opts.ApplyTo(products.AsQueryable());
                var response = new
                {
                    content = query.AsQueryable(),
                    totalCount = products.Count
                };
                return Ok(new BaseResponse<object>().successWithData(response).ToJson());
            }
            catch(Exception ex)
            {
                return Ok(new BaseResponse<string>().errWithData(ex.Message).ToJson());
            }
        }
        [HttpGet("{id}")]
        [Authorize]
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
