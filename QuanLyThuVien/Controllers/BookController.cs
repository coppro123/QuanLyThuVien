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

			var filteredProducts = await products.ToListAsync();

			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View(filteredProducts);
		}

		[HttpGet("[controller]/FindByCategory/{id}")]
		public async Task<IActionResult> FindByCategory(int id)
		{
			var categories = _categoryRepository.GetAll();
			ViewBag.Categories = new SelectList(categories, "Id", "Name");

			if(id == -1)
			{
				return RedirectToAction(nameof(Index));
			}

			var books = await _bookRepository.GetByCategory(id);
			return View("Index", books);

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
