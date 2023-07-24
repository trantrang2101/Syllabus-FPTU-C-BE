using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebView.Models;

namespace WebView.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ManagerController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Curriculum()
        {
            return View();
        }

        public IActionResult Term(){
            return View();
        }
        public IActionResult Account()
        {
            return View();
        }
        public IActionResult Assessment()
        {
            return View();
        }
        public IActionResult Category()
        {
            return View();
        }
        public IActionResult Subject()
        {
            return View();
        }
        public IActionResult Role()
        {
            return View();
        }

        public IActionResult Class()
        {
            return View();
        }

        public IActionResult Combo()
        {
            return View();
        }

        public IActionResult Course()
        {
            return View();
        }

        public IActionResult Department()
        {
            return View();
        }

        public IActionResult Major()
        {
            return View();
        }

        public IActionResult Sidebar()
        {
            return View();
        }
    }
}