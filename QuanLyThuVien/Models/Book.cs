using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
	public class Book
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Author { get; set; }

		public int PublisherId { get; set; }
		public Publisher? Publisher { get; set; }

		public int YearPublished { get; set; }

		public int CategoryId { get; set; }

		public Category? Category { get; set; }

		public int Quantity { get; set; }

		public string? ImageUrl { get; set; }
	}
}

