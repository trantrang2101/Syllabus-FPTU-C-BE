using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebView.Controllers
{
    public class CurriculumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetSessionData(string key, string value)
        {
            HttpContext.Session.SetString(key,value);
            return Json(value);
        }
        public IActionResult Detail(int id)
        {
            string dataJson = HttpContext.Session.GetString("Detail");
            if (!string.IsNullOrEmpty(dataJson))
            {
                CurriculumDTO value = JsonConvert.DeserializeObject<CurriculumDTO>(dataJson);
                ViewData["Title"] = value.Code;
                ViewData["SubTitle"] = value.Name;
                ViewData["Icon"] = "fa-solid fa-book-bookmark";
                HttpContext.Session.Remove("Detail");
                return View();
            }
            else
            {
                return RedirectToAction("List", "Curriculum");
            }
        }
        public IActionResult List()
        {
            return View();
        }
    }
}
