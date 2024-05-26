using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactController (IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactRepository.AddAsync(contact);
                TempData["SuccessMessage"] = "Thông tin của bạn đã được ghi nhận thành công.";
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }
    }
}
