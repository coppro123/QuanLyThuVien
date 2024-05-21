using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using System.Collections.Generic;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DashboardManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var top10Books = (from loan in _context.Loans
                              join book in _context.Books on loan.BookId equals book.Id
                              group book by book.Title into bookGroup
                              orderby bookGroup.Count() descending
                              select new
                              {
                                  Title = bookGroup.Key,
                                  SoLuongMuon = bookGroup.Count()
                              }).Take(10).ToList();

            ViewBag.Top10Books = top10Books;
            return View();
        }

        public ActionResult Top10Books()
        {
            var top10Books = (from loan in  _context.Loans
                              join book in  _context.Books on loan.BookId equals book.Id
                              group book by book.Title into bookGroup
                              orderby bookGroup.Count() descending
                              select new
                              {
                                  Title = bookGroup.Key,
                                  SoLuongMuon = bookGroup.Count()
                              }).Take(10).ToList();

            ViewBag.Top10Books = top10Books;

            return View();
        }
    }
}
