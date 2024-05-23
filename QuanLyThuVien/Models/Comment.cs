namespace QuanLyThuVien.Models
{
	public class Comment
	{
		public int Id { get; set; }

		public int BlogId { get; set; }

		public Blog? Blog { get; set; }

		public string UserId { get; set; }

		
	}
}
