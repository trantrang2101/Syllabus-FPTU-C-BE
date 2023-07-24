using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebView.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            Error value = new Error();
            if (statusCode == 403)
            {
                value = new Error() { Code = "403", Title = "Quyền không đủ", Message = "Bạn không có quyền truy cập chức năng này" };
            }
            else
            {
                value = new Error(){ Code= "404", Title = "Không tìm thấy trang", Message = "Đường dẫn này không được tìm thấy trong hệ thống" };
            }
            ViewData["Title"] = value.Title;
            ViewData["Code"] = value.Code;
            ViewData["Message"] = value.Message;
            HttpContext.Session.Remove("Error");
            return View();
        }

    }
}
