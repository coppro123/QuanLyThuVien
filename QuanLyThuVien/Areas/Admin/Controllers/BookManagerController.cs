using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles =SD.Role_Admin)]
	public class BookManagerController : Controller
	{
		private readonly IBookRepository _bookRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IPublisherRepository _publisherRepository;
		public BookManagerController(IBookRepository bookRepository, ICategoryRepository categoryRepository, IPublisherRepository publisherRepository)
		{
			_bookRepository = bookRepository;
			_categoryRepository = categoryRepository;
			_publisherRepository = publisherRepository;
		}

		// GET: Book
		public async Task<IActionResult> Index(string searchString)
		{
			var products = _bookRepository.GetAll();

			if (!String.IsNullOrEmpty(searchString))
			{
				products = products.Where(s => s.Title.Contains(searchString));
			}

			return View(await products.ToListAsync());
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

		// GET: Book/Create
		public async Task<IActionResult> Create()
		{
			var categories = await _categoryRepository.GetAllAsync();
			var publishers = await _publisherRepository.GetAllAsync();

			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			ViewBag.Publishers = new SelectList(publishers, "Id", "Name");
			return View();
		}

		// POST: Book/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		public async Task<IActionResult> Create(Book book, IFormFile
		imageUrl)
		{
			if (ModelState.IsValid)
			{
				if (imageUrl != null)
				{
					// Lưu hình ảnh đại diện tham khảo bài 02 hàm SaveImage
					book.ImageUrl = await SaveImage(imageUrl);
				}
				_bookRepository.AddAsync(book);
				return RedirectToAction(nameof(Index));
			}

			// Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
			var categories = await _categoryRepository.GetAllAsync();
			var publishers = await _publisherRepository.GetAllAsync();

			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			ViewBag.Publishers = new SelectList(publishers, "Id", "Name");
			return View();
		}

		private async Task<string> SaveImage(IFormFile image)
		{
			var savePath = Path.Combine("wwwroot/images", image.FileName); //

			using (var fileStream = new FileStream(savePath, FileMode.Create))
			{
				await image.CopyToAsync(fileStream);
			}
			return "/images/" + image.FileName; // Trả về đường dẫn tương đối
		}
		// GET: Book/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			var product = await _bookRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			var categories = await _categoryRepository.GetAllAsync();
			var publishers = await _publisherRepository.GetAllAsync();

			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			ViewBag.Publishers = new SelectList(publishers, "Id", "Name");

			return View(product);
		}
		// Xử lý cập nhật sản phẩm
		[HttpPost]
		public async Task<IActionResult> Edit(int id, Book product,
		IFormFile imageUrl)
		{
			ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho

			if (id != product.Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				var existingProduct = await
				_bookRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync

				if (imageUrl == null)
				{
					product.ImageUrl = existingProduct.ImageUrl;
				}
				else
				{
					// Lưu hình ảnh mới
					product.ImageUrl = await SaveImage(imageUrl);
				}
				// Cập nhật các thông tin khác của sản phẩm

				existingProduct.Title = product.Title;
				existingProduct.Author = product.Author;
				existingProduct.PublisherId = product.PublisherId;
				existingProduct.CategoryId = product.CategoryId;
				existingProduct.YearPublished = product.YearPublished;
				existingProduct.ImageUrl = product.ImageUrl;
				existingProduct.Quantity = product.Quantity;

				await _bookRepository.UpdateAsync(existingProduct);
				return RedirectToAction(nameof(Index));
			}
			var categories = await _categoryRepository.GetAllAsync();
			ViewBag.Categories = new SelectList(categories, "Id", "Name");


			return View(product);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var product = await _bookRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}
		// Xử lý xóa sản phẩm
		[HttpPost, ActionName("DeleteConfirmed")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _bookRepository.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
