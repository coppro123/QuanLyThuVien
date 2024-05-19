using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly ApplicationDbContext _context;
        public PublisherRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Publisher>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.Publishers
            .ToListAsync();
        }
        public async Task<Publisher> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Publisher> GetAll()
        {
            return _context.Publishers;
        }
    }
}
