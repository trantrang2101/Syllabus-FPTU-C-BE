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

        public IActionResult Term()
        {
            return View();
        }
    }
}