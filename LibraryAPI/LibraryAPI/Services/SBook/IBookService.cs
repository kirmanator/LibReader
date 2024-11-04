using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
namespace LibraryAPI.Services.SBook
{
    public interface IBookService : IGenericService<Book> {
        Task<bool> IsAvailable(int bookId);
        Task<List<Book>> GetAll(List<int> bookIds);
        Task<List<Book>> GetPaginated(int pageSize, int pageNumber);
        Task<List<Book>> GetFeatured();
        Task<List<Book>> Search(string searchQuery);
        Task SetAvailable(int id, bool available);
        Task<int> GetTotal();
    }
}
