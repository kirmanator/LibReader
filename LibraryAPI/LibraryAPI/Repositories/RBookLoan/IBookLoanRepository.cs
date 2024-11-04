using LibraryAPI.Models;

namespace LibraryAPI.Repositories.RBookLoan {
    public interface IBookLoanRepository : IGenericRepository<BookLoan> {
        Task<List<BookLoan>> GetAllByUserIdAsync(int userId);
    }
}
