using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<Loan> GetByIdAsync(int id);
        Task AddAsync(Loan laon);
        Task UpdateAsync(Loan loan);
        Task DeleteAsync(int id);
        IQueryable<Loan> GetAll();
    }
}
