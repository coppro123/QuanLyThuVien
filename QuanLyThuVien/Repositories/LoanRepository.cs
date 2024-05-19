using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;
        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.Loans
            .Include(p => p.Book)
            .Include(p => p.Reader)
            .ToListAsync();
        }
        public async Task<Loan> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.Loans
            .Include(p => p.Book)
            .Include(p => p.Reader)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Loan> GetAll()
        {
            return _context.Loans
                .Include(p => p.Book)
                .Include(p => p.Reader);
        }
    }
}
