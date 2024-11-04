using LibraryAPI.Models;
using LibraryAPI.Repositories.RBook;

namespace LibraryAPI.Services.SBook
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public async Task<Book?> GetById(int id)
        {
            return await bookRepository.GetByIdAsync(id);
        }

        public async Task<List<Book>> GetAll()
        {
            return await bookRepository.GetAllAsync();
        }

        public async Task<List<Book>> GetAll(List<int> bookIds) {
            return await bookRepository.GetAllAsync(bookIds);
        }

        public async Task<bool> ExistsById(int id)
        {
            return await bookRepository.ExistsByIdAsync(id);
        }

        public async Task<bool> IsAvailable(int id) {
            return await bookRepository.IsAvailableAsync(id);
        }

        public async Task<List<Book>> GetPaginated(int pageSize, int pageNumber) {
            return await bookRepository.GetPaginatedAsync(pageSize, pageNumber);
        }

        public async Task<List<Book>> GetFeatured() {
            return await bookRepository.GetFeaturedAsync();
        }

        public async Task<int> GetTotal() {
            return await bookRepository.GetTotal();
        }

        public async Task Insert(Book userInfo)
        {
            await bookRepository.InsertAsync(userInfo);
        }

        public async Task<List<Book>> Search(string searchQuery) {
            return await bookRepository.SearchAsync(searchQuery);
        }
        public async Task Update(Book userInfo)
        {
            await bookRepository.UpdateAsync(userInfo);
        }
        public async Task SetAvailable(int id, bool available) {
            await bookRepository.SetAvailableAsync(id, available);
        }
        public async Task Delete(int id)
        {
            await bookRepository.DeleteAsync(id);
        }

    }
}
