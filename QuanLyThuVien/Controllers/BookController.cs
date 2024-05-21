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

			var filteredProducts = await products.ToListAsync(); // Chờ đợi tìm kiếm được áp dụng trước khi lấy dữ liệu từ cơ sở dữ liệu

			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View(filteredProducts);
		}


		public async Task<IActionResult> FilterByCategory(int id)
		{
			var categories = _categoryRepository.GetAll();
			IEnumerable<Book> products;
			
			if (id == -1)
			{
				return RedirectToAction("Index"); // Chuyển hướng đến action Index
			}
			else
			{
				products = await _bookRepository.GetByCategory(id);
			}

			ViewBag.Categories = new SelectList(categories, "Id", "Name"); // Sử dụng danh sách đã được lấy từ Index

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
