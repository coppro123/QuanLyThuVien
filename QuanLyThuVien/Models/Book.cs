using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
	public class Book
	{
		public int Id { get; set; }

		[Required]
		[StringLength(255)]
		public string Title { get; set; }

		[StringLength(100)]
		public string Author { get; set; }

		public int PublisherId { get; set; }
		public Publisher Publisher { get; set; }

		public int YearPublished { get; set; }

		public int CategoryId { get; set; }

		public Category Category { get; set; }

		public int Quantity { get; set; }
	}
}

