using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public interface IReaderRepository
    {
        Task<IEnumerable<Reader>> GetAllAsync();
        Task<Reader> GetByIdAsync(int id);
        Task AddAsync(Reader reader);
        Task UpdateAsync(Reader reader);
        Task DeleteAsync(int id);
        IQueryable<Reader> GetAll();
    }
}
