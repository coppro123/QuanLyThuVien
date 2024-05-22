using Microsoft.AspNetCore.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class GPTViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
