using LibraryAPI.Dto;
using LibraryAPI.Models;
using LibraryAPI.Repositories.RUserReview;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Services.SUserReview
{
    public class UserReviewService : IUserReviewService
    {
        private readonly IUserReviewRepository userReviewRepository;
        public UserReviewService(IUserReviewRepository userReviewRepository)
        {
            this.userReviewRepository = userReviewRepository;
        }

        public async Task<UserReview?> GetById(int id)
        {
            return await userReviewRepository.GetByIdAsync(id);
        }

        public async Task<List<UserReview>> GetAll()
        {
            return await userReviewRepository.GetAllAsync();
        }
        public async Task<List<UserReview>> GetAllByUser(int userId) {
            return await userReviewRepository.GetAllByUserAsync(userId);
        }
        public async Task<List<UserReviewDto>> GetAllByBook(int bookId) {
            return await userReviewRepository.GetAllByBookAsync(bookId);
        }

        public async Task<bool> ExistsById(int id)
        {
            return await userReviewRepository.ExistsByIdAsync(id);
        }

        public async Task Insert(UserReview userInfo)
        {
            await userReviewRepository.InsertAsync(userInfo);
        }
        public async Task Update(UserReview userInfo)
        {
            await userReviewRepository.UpdateAsync(userInfo);
        }
        public async Task Delete(int id)
        {
            await userReviewRepository.DeleteAsync(id);
        }

    }
}
