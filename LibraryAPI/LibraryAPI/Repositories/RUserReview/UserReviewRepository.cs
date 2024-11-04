using Dapper;
using LibraryAPI.Config;
using LibraryAPI.Dto;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Repositories.RUserReview
{
    public class UserReviewRepository : IUserReviewRepository
    {
        private readonly ConnectionContext context;
        public UserReviewRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public async Task<UserReview?> GetByIdAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {

                string query = """
                SELECT * FROM user_review
                WHERE user_review_id = @id
                """;
                UserReview? userReview = await connection.QueryFirstOrDefaultAsync<UserReview>(query, new
                {
                    id
                });
                return userReview;
            }
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                SELECT
                    CASE WHEN EXISTS( SELECT 1 FROM user_review WHERE user_review_id = @id ) THEN 1
                    ELSE 0
                    END
                """;
                bool exists = await connection.QueryFirstAsync<bool>(query, new
                {
                    id
                });
                return exists;
            }
        }

        public async Task<List<UserReview>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                SELECT * FROM user_review
                """;
                IEnumerable<UserReview> userReviews = await connection.QueryAsync<UserReview>(query);
                return userReviews.ToList();
            }
        }

        public async Task<List<UserReview>> GetAllByUserAsync(int userId) {
            using (var connection = context.CreateConnection()) {
                string query = """
                SELECT * FROM user_review
                WHERE user_id = @userId
                """;
                IEnumerable<UserReview> userReviews = await connection.QueryAsync<UserReview>(query, new { userId });
                return userReviews.ToList();
            }
        }
        public async Task<List<UserReviewDto>> GetAllByBookAsync(int bookId) {
            using (var connection = context.CreateConnection())
            {
                string query = """
                SELECT ui.user_id, ui.username, ur.book_id, ur.customer_review, ur.rating FROM user_review ur
                INNER JOIN user_info ui ON (ur.user_id = ui.user_id)
                WHERE ur.book_id = @bookId
                """;
                IEnumerable<UserReviewDto> userReviews = await connection.QueryAsync<UserReviewDto>(query, new { bookId });
                return userReviews.ToList();
            }
        }


        public async Task<int> InsertAsync(UserReview entity)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                INSERT INTO user_review (user_id, book_id, customer_review, rating)
                VALUES (@UserId, @BookId, @CustomerReview, @Rating)
                """;
                return await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task<int> UpdateAsync(UserReview entity)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                UPDATE user_review
                SET user_id = @UserId,
                book_id = @BookId,
                customer_review = @CustomerReview,
                rating = @Rating
                WHERE user_review_id = @UserReviewId
                """;
                return await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                DELETE FROM user_review WHERE user_review_id = @id
                """;
                return await connection.ExecuteAsync(query, new
                {
                    id
                });
            }
        }
    }
}

