using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly ApplicationDbContext _context;
        public ReaderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Reader>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.Readers
            .ToListAsync();
        }
        public async Task<Reader> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.Readers
            .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Reader reader)
        {
            _context.Readers.Add(reader);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Reader reader)
        {
            _context.Readers.Update(reader);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var reader = await _context.Readers.FindAsync(id);
            _context.Readers.Remove(reader);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Reader> GetAll()
        {
            return _context.Readers;
        }
    }
}
