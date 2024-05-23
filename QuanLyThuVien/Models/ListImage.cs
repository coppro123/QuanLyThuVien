using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
	public class ListImage
	{
		public int Id { get; set; }

		public string ImageUrl { get; set; }

		public int BookId { get; set; }
		public Book? Book { get; set; }
	}
}