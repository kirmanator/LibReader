using LibraryAPI.Models;
using LibraryAPI.Repositories.RBookLoan;

namespace LibraryAPI.Services.SBookLoan
{
    public class BookLoanService : IBookLoanService
    {
        private readonly IBookLoanRepository bookLoanRepository;
        public BookLoanService(IBookLoanRepository bookLoanRepository)
        {
            this.bookLoanRepository = bookLoanRepository;
        }

        public async Task<BookLoan?> GetById(int id)
        {
            return await bookLoanRepository.GetByIdAsync(id);
        }

        public async Task<List<BookLoan>> GetAll()
        {
            return await bookLoanRepository.GetAllAsync();
        }

        public async Task<List<BookLoan>> GetAllByUserId(int userId) {
            return await bookLoanRepository.GetAllByUserIdAsync(userId);
        }

        public async Task<bool> ExistsById(int id)
        {
            return await bookLoanRepository.ExistsByIdAsync(id);
        }

        public async Task Insert(BookLoan userInfo)
        {
            await bookLoanRepository.InsertAsync(userInfo);
        }
        public async Task Update(BookLoan userInfo)
        {
            await bookLoanRepository.UpdateAsync(userInfo);
        }
        public async Task Delete(int id)
        {
            await bookLoanRepository.DeleteAsync(id);
        }

    }
}
