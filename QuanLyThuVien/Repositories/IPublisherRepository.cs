using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAllAsync();
        Task<Publisher> GetByIdAsync(int id);
        Task AddAsync(Publisher publisher);
        Task UpdateAsync(Publisher publisher);
        Task DeleteAsync(int id);

        IQueryable<Publisher> GetAll();
    }
}
