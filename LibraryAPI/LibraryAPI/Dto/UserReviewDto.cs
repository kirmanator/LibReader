using Dapper.FluentMap.Mapping;

namespace LibraryAPI.Dto {
    public class UserReviewDto {
        public int UserId {
            get; set;
        }
        public string? Username { get; set; }
        public int BookId {
            get; set;
        }
        public string? CustomerReview {
            get; set;
        }
        public int Rating {
            get; set;
        }

        public UserReviewDto() {
        }
        public UserReviewDto(int userId, string username, int bookId, string customerReview, int rating) {
            UserId = userId;
            Username = username;
            BookId = bookId;
            CustomerReview = customerReview;
            Rating = rating;
        }
    }
    internal class UserReviewMap : EntityMap<UserReviewDto> {
        internal UserReviewMap() {
            Map(entity => entity.UserId).ToColumn("user_id");
            Map(entity => entity.Username).ToColumn("username");
            Map(entity => entity.BookId).ToColumn("book_id");
            Map(entity => entity.CustomerReview).ToColumn("customer_review");
            Map(entity => entity.Rating).ToColumn("rating");
        }
    }
}
