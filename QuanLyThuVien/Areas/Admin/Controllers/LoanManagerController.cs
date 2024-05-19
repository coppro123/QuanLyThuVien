using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;
using QuanLyThuVien.Repositories;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
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
            ViewBag.Books = new SelectList(books, "Id", "Name");
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
                await _loanRepository.AddAsync(loan);
                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var readers = await _readerRepository.GetAllAsync();
            var books = await _bookRepository.GetAllAsync();

            ViewBag.Categories = new SelectList(readers, "Id", "FullName");
            ViewBag.Publishers = new SelectList(books, "Id", "Name");
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
            var loan = await _bookRepository.GetByIdAsync(id);
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
            await _bookRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
