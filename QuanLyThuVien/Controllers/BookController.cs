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
			// Fetch data from the database without applying the filter
			var products = await _bookRepository.GetAll().ToListAsync();
			var categories = await _categoryRepository.GetAll().ToListAsync();

			if (!String.IsNullOrEmpty(searchString))
			{
				// Normalize the search string
				string normalizedSearchString = StringUtils.RemoveDiacritics(searchString.ToLower());

				// Apply the filter in memory
				products = products.Where(s =>
					StringUtils.RemoveDiacritics(s.Title.ToLower()).Contains(normalizedSearchString)
				).ToList();
			}

			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View(products);
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
