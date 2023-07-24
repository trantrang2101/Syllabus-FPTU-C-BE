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
            }
            return View();
        }

        public IActionResult Subject(int id)
        {
            string dataJson = HttpContext.Session.GetString("Subject");
            Console.WriteLine(dataJson);
            if (!string.IsNullOrEmpty(dataJson))
            {
                CurriculumDetailDTO value = JsonConvert.DeserializeObject<CurriculumDetailDTO>(dataJson);
                ViewData["Title"] = value.Curriculum.Code;
                ViewData["SubTitle"] = value.Subject.Code+" - "+value.Subject.Name;
                ViewData["Icon"] = "fa-solid fa-book-bookmark";
                HttpContext.Session.Remove("Subject");
            }
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
    }
}
