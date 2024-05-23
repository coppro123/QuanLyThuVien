using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyThuVien.Models
{
	public class Blog
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string ImageUrl { get; set; }

		public string Content { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

    }
}
