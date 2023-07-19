using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebView.Controllers
{
    public class CurriculumController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SetViewData(CurriculumDTO value)
        {
            ViewData["Title"] = "Home Page";
            ViewData["SubTitle"] = "Home Page";
            ViewData["Icon"] = "fa-solid fa-book-bookmark";
            return new EmptyResult();
        }
        public IActionResult Detail()
        {
            return View();
        }
    }
}
