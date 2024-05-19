using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ReaderManagerController : Controller
    {
        private readonly IReaderRepository _readerRepository;

        public ReaderManagerController(IReaderRepository readerRepository)
        {
            _readerRepository = readerRepository;
        }

        // GET: Book
        public async Task<IActionResult> Index(string searchString)
        {
            var products = _readerRepository.GetAll();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.FullName.Contains(searchString));
            }

            return View(await products.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var reader = await _readerRepository.GetByIdAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // GET: Book/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(Reader reader, IFormFile imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện tham khảo bài 02 hàm SaveImage
                    reader.ImageUrl = await SaveImage(imageUrl);
                }
                await _readerRepository.AddAsync(reader);
                return RedirectToAction(nameof(Index));
            }

            
            return View();
        }

        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/avatar", image.FileName); //

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/avatar/" + image.FileName; // Trả về đường dẫn tương đối
        }
        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _readerRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Reader reader,
        IFormFile imageUrl)
        {
            ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho

            if (id != reader.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingProduct = await _readerRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync

                if (imageUrl == null)
                {
                    reader.ImageUrl = existingProduct.ImageUrl;
                }
                else
                {
                    // Lưu hình ảnh mới
                    reader.ImageUrl = await SaveImage(imageUrl);
                }
                // Cập nhật các thông tin khác của sản phẩm

                
                existingProduct.FullName = reader.FullName;
                existingProduct.DateOfBirth = reader.DateOfBirth;
                existingProduct.Address = reader.Address;
                existingProduct.PhoneNumber = reader.PhoneNumber;
                existingProduct.ImageUrl = reader.ImageUrl;
                existingProduct.Email = reader.Email;

                await _readerRepository.UpdateAsync(existingProduct);
                return RedirectToAction(nameof(Index));
            }
            


            return View(reader);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _readerRepository.GetByIdAsync(id);
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
            await _readerRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
