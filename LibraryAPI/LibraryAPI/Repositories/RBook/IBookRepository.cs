using LibraryAPI.Models;

namespace LibraryAPI.Repositories.RBook
{
    public interface IBookRepository : IGenericRepository<Book> {
        Task<bool> IsAvailableAsync(int bookId);
        Task<List<Book>> GetPaginatedAsync(int pageSize, int pageNumber);
        Task<List<Book>> GetAllAsync(List<int> bookIds);
        Task<List<Book>> GetFeaturedAsync();
        Task<List<Book>> SearchAsync(string searchQuery);
        Task SetAvailableAsync(int id, bool available);
        Task<int> GetTotal();
    }
}
