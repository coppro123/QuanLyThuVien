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

            var topCategories = from loan in _context.Loans
                                join book in _context.Books on loan.BookId equals book.Id
                                join category in _context.Categories on book.CategoryId equals category.Id
                                group category by category.Name into categoryGroup
                                orderby categoryGroup.Count() descending
                                select new
                                {
                                    Category = categoryGroup.Key,
                                    LoanCount = categoryGroup.Count()
                                };
            ViewBag.TopCategories = topCategories;


            var  NumberOfBooks = _context.Books.Count();

            ViewBag.NumberOfBooks = NumberOfBooks;

            var NumberOfReaders = _context.Readers.Count();   
            ViewBag.NumberOfReaders = NumberOfReaders;

            var NumberOfBlogs = _context.Blog.Count();
            ViewBag.NumberOfBlogs = NumberOfBlogs;

            return View();
        }

    }
}
