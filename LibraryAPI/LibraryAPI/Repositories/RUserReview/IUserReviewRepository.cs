using LibraryAPI.Dto;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Repositories.RUserReview
{
    public interface IUserReviewRepository : IGenericRepository<UserReview> {
        Task<List<UserReview>> GetAllByUserAsync(int userId);
        Task<List<UserReviewDto>> GetAllByBookAsync(int bookId);

    }
}
