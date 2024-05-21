using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPublisherRepository _publisherRepository;
        public BookController(IBookRepository bookRepository, ICategoryRepository categoryRepository, IPublisherRepository publisherRepository)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _publisherRepository = publisherRepository;
        }

        // GET: Book
        public async Task<IActionResult> Index(string searchString)
        {
            var products = _bookRepository.GetAll();
            var categories = _categoryRepository.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Title.Contains(searchString));
            }

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(await products.ToListAsync());
        }

        public async Task<IActionResult> FilterByCategory(int id)
        {
            IEnumerable<Book> products;
			var categories = _categoryRepository.GetAll();
			if (id == 0)
            {
                products = await _bookRepository.GetAll().ToListAsync();
            }
            else
            {
                products = await _bookRepository.GetByCategory(id);
            }
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View(products);
        }


        // GET: Book/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
    }
}
