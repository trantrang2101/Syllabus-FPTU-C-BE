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

        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult List()
        {
            return View();
        }
    }
}
