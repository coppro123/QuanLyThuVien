using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVien.Models
{
	public class Loan
	{
		public int Id { get; set; }

		public int BookId { get; set; }

		public Book Book { get; set; }

		public int ReaderId { get; set; }

		public Reader Reader { get; set; }	

		[DataType(DataType.Date)]
		public DateTime LoanDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime DueDate { get; set; }

		[DataType(DataType.Date)]
		public DateTime? ReturnDate { get; set; }

		[StringLength(50)]
		public string Status { get; set; }
	}
}
