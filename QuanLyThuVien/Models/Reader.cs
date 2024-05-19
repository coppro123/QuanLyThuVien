using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
	public class Reader
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(100)]
		public string LastName { get; set; }

		[DataType(DataType.Date)]
		public DateTime DateOfBirth { get; set; }

		[StringLength(255)]
		public string Address { get; set; }

		[StringLength(20)]
		public string PhoneNumber { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		[DataType(DataType.Date)]
		public DateTime MembershipDate { get; set; }
	}
}
