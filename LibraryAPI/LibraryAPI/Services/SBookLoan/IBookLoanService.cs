using LibraryAPI.Models;

namespace LibraryAPI.Services.SBookLoan
{
    public interface IBookLoanService : IGenericService<BookLoan> {
        Task<List<BookLoan>> GetAllByUserId(int userId);
    }
}
