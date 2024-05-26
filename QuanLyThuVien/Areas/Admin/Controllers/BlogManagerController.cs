using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]
	public class BlogManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public BlogManagerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
			_userManager = userManager;
		}

        // GET: Admin/BlogManager
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Blog.Include(b => b.ApplicationUser).OrderByDescending(p => p.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/BlogManager/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Admin/BlogManager/Create
        public IActionResult Create()
        {           
            return View();
        }

        // POST: Admin/BlogManager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]		
		public async Task<IActionResult> Create(Blog blog)
		{

			// Set UserId to the current user's name
			var user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				ModelState.AddModelError("", "Vui lòng đăng nhập hoặc đăng ký để tạo bài viết");
				return View(blog);
			}

			blog.UserId = user.Id;

			if (ModelState.IsValid)
			{
				_context.Blog.Add(blog);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(blog);

		}

		// GET: Admin/BlogManager/Edit/5
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", blog.UserId);
            return View(blog);
        }

		// POST: Admin/BlogManager/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Blog blog)
		{
			var user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				ModelState.AddModelError("", "Vui lòng đăng nhập hoặc đăng ký để sửa bài viết");
				return View();
			}

			if (id != blog.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					var existingProduct = _context.Blog.FirstOrDefault(p => p.Id == id);
					
					existingProduct.Title = blog.Title;
					existingProduct.ImageUrl = blog.ImageUrl;
					existingProduct.Content = blog.Content;


					_context.Blog.Update(existingProduct);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!BlogExists(blog.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(blog);
		}

		// GET: Admin/BlogManager/Delete/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .Include(b => b.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Admin/BlogManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blog.FindAsync(id);
            if (blog != null)
            {
                _context.Blog.Remove(blog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blog.Any(e => e.Id == id);
        }
    }
}
