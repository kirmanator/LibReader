using LibraryAPI.Dto;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Services.SUserReview
{
    public interface IUserReviewService : IGenericService<UserReview> {
        Task<List<UserReview>> GetAllByUser(int userId);
        Task<List<UserReviewDto>> GetAllByBook(int bookId);

    }
}
