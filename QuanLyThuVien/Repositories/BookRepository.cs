using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
            .Include(p => p.Category) 
            .Include(p => p.Publisher)
            .OrderByDescending(p => p.Id)
            .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByName(string name)
        {
            return await _context.Books
                .Include(p => p.Category)
                .Include(p => p.Publisher)
                .Where(p => p.Title.Contains(name, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }


        public async Task<Book> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.Books
            .Include(p => p.Category)
            .Include(p => p.Publisher)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Books.FindAsync(id);
            _context.Books.Remove(product);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Book> GetAll()
        {
            return _context.Books
                .Include(p => p.Category)
                .Include(p => p.Publisher)
                .OrderByDescending(p => p.Id);
        }

        public async Task<IEnumerable<Book>> GetByCategory(int id)
        {
            // Retrieve books by category ID along with associated category and publisher
            var books = await _context.Books
                .Include(p => p.Category)
                .Include(p => p.Publisher)
                .Where(p => p.CategoryId == id)
                .ToListAsync();

            return books;
        }

    }
}
