using BusinessObject.Models;
using DataAccess.Repositories.Interface;
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
        public IActionResult List()
        {
            List<D> products = _repository.GetAll();
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult Detail(int id)
        {
            D dto = _repository.Get(id);
            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            D dto = _repository.Get(id);
            if (dto == null)
            {
                return NotFound();
            }
            _repository.Delete(id);
            return Ok();
        }
        [HttpPost]
        public IActionResult Add(D dto)
        {
            try
            {
                D p = _repository.Add(dto);
                return Ok(p);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Update(D dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            D p = _repository.Update(dto);
            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }
    }
}
