using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
	public class Publisher
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(255)]
		public string Address { get; set; }

		[StringLength(20)]
		public string PhoneNumber { get; set; }

		[EmailAddress]
		public string Email { get; set; }
	}
}
