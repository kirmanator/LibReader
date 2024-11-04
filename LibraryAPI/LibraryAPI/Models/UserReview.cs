using Dapper.FluentMap.Mapping;

namespace LibraryAPI.Models {
    public class UserReview {

        public int UserReviewId {
            get; set;
        }
        public int UserId {
            get; set;
        }
        public int BookId {
            get; set;
        }
        public string? CustomerReview {
            get; set;
        }
        public int Rating {
            get; set;
        }
        public UserReview() {
        }
        public UserReview(int userReviewId, int userId, int bookId, string customerReview, int rating) {
            UserReviewId = userReviewId;
            UserId = userId;
            BookId = bookId;
            CustomerReview = customerReview;
            Rating = rating;
        }
    }

    internal class UserReviewMap : EntityMap<UserReview> {
        internal UserReviewMap() {
            Map(entity => entity.UserReviewId).ToColumn("user_review_id");
            Map(entity => entity.UserId).ToColumn("user_id");
            Map(entity => entity.BookId).ToColumn("book_id");
            Map(entity => entity.CustomerReview).ToColumn("customer_review");
            Map(entity => entity.Rating).ToColumn("rating");
        }
    }
}
