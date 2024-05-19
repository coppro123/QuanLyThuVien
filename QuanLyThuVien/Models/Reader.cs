using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
	public class Reader
	{
		public int Id { get; set; }

		
		public string FullName { get; set; }

		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }

		
		public string Address { get; set; }

		
		public string PhoneNumber { get; set; }

		
		public string Email { get; set; }

		public string? ImageUrl { get; set; }	
	}
}
