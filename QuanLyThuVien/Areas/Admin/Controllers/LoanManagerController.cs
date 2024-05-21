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
	public class LoanManagerController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReaderRepository _readerRepository;
        private readonly ILoanRepository _loanRepository;
        public LoanManagerController(IBookRepository bookRepository, IReaderRepository readerRepository, ILoanRepository loanRepository)
        {
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
            _loanRepository = loanRepository;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
			var books = await _bookRepository.GetAllAsync();
			var readers = await _readerRepository.GetAllAsync();

			ViewBag.Books = new SelectList(books, "Id", "Title");
			ViewBag.Readers = new SelectList(readers, "Id", "FullName");

			var loans = _loanRepository.GetAll();          
            return View(loans);
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }

        // GET: Book/Create
        public async Task<IActionResult> Create()
        {
            var readers = await _readerRepository.GetAllAsync();
            var books = await _bookRepository.GetAllAsync();

            ViewBag.Readers = new SelectList(readers, "Id", "FullName");
            ViewBag.Books = new SelectList(books, "Id", "Title");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Loan loan)
        {
            if (ModelState.IsValid)
            {
                loan.LoanDate = DateTime.Now;
                loan.Status = "Đang mượn";

                // Lấy sách được mượn từ cơ sở dữ liệu
                var book = await _bookRepository.GetByIdAsync(loan.BookId);
                if (book == null)
                {
                    // Xử lý khi không tìm thấy sách
                    return NotFound();
                }

                if (book.Quantity == 0)
                {
                    // Trả về BadRequest nếu không còn sách trong kho
                    return ("Đã hết sách trong kho");
                }
                // Giảm số lượng sách đi 1
                book.Quantity--;

                // Cập nhật sách trong cơ sở dữ liệu
                await _bookRepository.UpdateAsync(book);

                // Thêm mục mượn vào cơ sở dữ liệu
                await _loanRepository.AddAsync(loan);

                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var readers = await _readerRepository.GetAllAsync();
            var books = await _bookRepository.GetAllAsync();

            ViewBag.Readers = new SelectList(readers, "Id", "FullName");
            ViewBag.Books = new SelectList(books, "Id", "Title");
            return View();
        }



        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            var readers = await _readerRepository.GetAllAsync();
            var books = await _bookRepository.GetAllAsync();

            ViewBag.Categories = new SelectList(readers, "Id", "FullName");
            ViewBag.Publishers = new SelectList(books, "Id", "Name");
            return View();
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Loan loan)
        {
            ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho

            if (id != loan.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingLoan = await _loanRepository.GetByIdAsync(id); 

                existingLoan.BookId = loan.BookId;
                existingLoan.ReaderId = loan.ReaderId;
                existingLoan.LoanDate = loan.LoanDate;
                existingLoan.DueDate = loan.DueDate;
                existingLoan.ReturnDate = loan.ReturnDate;
                existingLoan.Status = loan.Status;              

                await _loanRepository.UpdateAsync(existingLoan);
                return RedirectToAction(nameof(Index));
            }
            var readers = await _readerRepository.GetAllAsync();
            var books = await _bookRepository.GetAllAsync();

            ViewBag.Categories = new SelectList(readers, "Id", "FullName");
            ViewBag.Publishers = new SelectList(books, "Id", "Name");
            return View(loan);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }
        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Lấy thông tin mượn để biết sách nào đã được mượn
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            // Tăng số lượng sách lên 1
            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book != null)
            {
                book.Quantity++;
                await _bookRepository.UpdateAsync(book);
            }

            // Xóa mượn khỏi cơ sở dữ liệu
            await _loanRepository.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
