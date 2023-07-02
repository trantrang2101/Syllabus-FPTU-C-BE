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
            BaseResponse<List<D>> response = new BaseResponse<List<D>>();
            List<D> products = _repository.GetAll();
            if (products == null)
            {
                return NotFound(response.errWithData(products).ToJson());
            }

            return Ok(response.successWithData(products).ToJson());
        }
        [HttpGet("{id}")]
        [Authorize]
        public virtual IActionResult Detail(int id)
        {
            BaseResponse<D> response = new BaseResponse<D>();
            D dto = _repository.Get(id);
            if (dto == null)
            {
                return NotFound(response.errWithData(dto).ToJson());
            }

            return Ok(response.successWithData(dto).ToJson());
        }
        [HttpDelete("{id}")]
        [Authorize]
        public virtual IActionResult Delete(int id)
        {
            BaseResponse<D> response = new BaseResponse<D>();
            D dto = _repository.Get(id);
            if (dto == null)
            {
                return NotFound(response.errWithData(dto).ToJson());
            }
            _repository.Delete(id);
            return Ok(response.successWithData(dto).ToJson());
        }
        [HttpPost]
        [Authorize]
        public virtual IActionResult Add(D dto)
        {
            BaseResponse<D> response = new BaseResponse<D>();
            try
            {
                D p = _repository.Add(dto);
                return Ok(response.successWithData(p).ToJson());
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(response.errWithData(null).ToJson());
            }
        }
        [HttpPost]
        [Authorize]
        public virtual IActionResult Update(D dto)
        {
            BaseResponse<D> response = new BaseResponse<D>();
            if (dto == null)
            {
                return BadRequest(response.errWithData(null).ToJson());
            }
            D p = _repository.Update(dto);
            if (p == null)
            {
                return NotFound(response.errWithData(null).ToJson());
            }
            return Ok(response.successWithData(p).ToJson());
        }
    }
}
