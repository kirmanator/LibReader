using Dapper.FluentMap.Mapping;

namespace LibraryAPI.Models {
    public class BookLoan {
        public int LoanId {
            get; set;
        }
        public int UserId {
            get; set;
        }
        public int BookId {
            get; set;
        }
        public DateTime? CheckedOut {
            get; set;
        }
        public DateTime? CheckedIn {
            get; set;
        }

        public BookLoan() {
        }

        public BookLoan(int loanId, int userId, int bookId) {
            LoanId = loanId;
            UserId = userId;
            BookId = bookId;
        }
    }

    internal class BookLoanMap : EntityMap<BookLoan> {
        internal BookLoanMap() {
            Map(entity => entity.LoanId).ToColumn("loan_id");
            Map(entity => entity.UserId).ToColumn("user_id");
            Map(entity => entity.BookId).ToColumn("book_id");
            Map(entity => entity.CheckedOut).ToColumn("checked_out");
            Map(entity => entity.CheckedIn).ToColumn("checked_in");
        }
    }
}
