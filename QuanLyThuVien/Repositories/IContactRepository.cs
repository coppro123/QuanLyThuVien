using QuanLyThuVien.Models;

namespace QuanLyThuVien.Repositories
{
    public interface IContactRepository
    {
        Task AddAsync(Contact contact);
    }
}
