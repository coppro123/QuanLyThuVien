using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Publisher> Publishers { get; set; }
		public DbSet<Reader> Readers { get; set; }
		public DbSet<Loan> Loans { get; set; }
	    public DbSet<Category> Categories { get; set; }

		public DbSet<ListImage> ListImages { get; set; }	
	    public DbSet <Blog> Blog { get; set; }

		public DbSet<Comment> Comment { get; set; }

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	}
}
