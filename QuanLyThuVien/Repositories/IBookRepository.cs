﻿using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);

        IQueryable<Book> GetAll();
    }
}
