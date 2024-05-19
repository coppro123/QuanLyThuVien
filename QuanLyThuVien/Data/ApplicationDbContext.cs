using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Publisher> Publishers { get; set; }
		public DbSet<Reader> Members { get; set; }
		public DbSet<Loan> Loans { get; set; }
	    public DbSet<QuanLyThuVien.Models.Category> Category { get; set; } = default!;
	}
}
