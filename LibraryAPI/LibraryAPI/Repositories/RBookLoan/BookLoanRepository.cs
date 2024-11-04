using Dapper;
using LibraryAPI.Config;
using LibraryAPI.Models;

namespace LibraryAPI.Repositories.RBookLoan {
    public class BookLoanRepository : IBookLoanRepository {
        private readonly ConnectionContext context;
        public BookLoanRepository(ConnectionContext context) {
            this.context = context;
        }

        public async Task<BookLoan?> GetByIdAsync(int id) {
            using (var connection = context.CreateConnection()) {

                string query = """
                SELECT * FROM book_loan
                WHERE loan_id = @id
                """;
                BookLoan? bookLoan = await connection.QueryFirstOrDefaultAsync<BookLoan>(query, new {
                    id
                });
                return bookLoan;
            }
        }

        public async Task<bool> ExistsByIdAsync(int id) {
            using (var connection = context.CreateConnection()) {
                string query = """
                SELECT 
                    CASE WHEN EXISTS( SELECT 1 FROM book_loan WHERE loan_id = @id ) THEN 1
                    ELSE 0
                    END
                """;
                bool exists = await connection.QueryFirstAsync<bool>(query, new {
                    id
                });
                return exists;
            }
        }

        public async Task<List<BookLoan>> GetAllAsync() {
            using (var connection = context.CreateConnection()) {
                string query = """
                SELECT * FROM book_loan
                """;
                IEnumerable<BookLoan> bookLoans = await connection.QueryAsync<BookLoan>(query);
                return bookLoans.ToList();
            }
        }

        public async Task<List<BookLoan>> GetAllByUserIdAsync(int id) {
            using (var connection = context.CreateConnection()) {
                string query = """
                SELECT * FROM book_loan
                WHERE user_id = @id
                ORDER BY checked_out DESC
                """;
                IEnumerable<BookLoan> bookLoans = await connection.QueryAsync<BookLoan>(query, new { id });
                return bookLoans.ToList();
            }
        }

        public async Task<int> InsertAsync(BookLoan entity) {
            using (var connection = context.CreateConnection()) {
                string query = """
                INSERT INTO book_loan (user_id, book_id)
                VALUES (@UserId, @BookId)
                """;
                return await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task<int> UpdateAsync(BookLoan entity) {
            using (var connection = context.CreateConnection()) {
                string query = """
                UPDATE book_loan
                SET checked_in = @CheckedIn
                WHERE loan_id = @LoanId
                """;
                return await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task<int> DeleteAsync(int id) {
            using (var connection = context.CreateConnection()) {
                string query = """
                DELETE FROM book_loan WHERE loan_id = @id
                """;
                return await connection.ExecuteAsync(query, new {
                    id
                });
            }
        }
    }
}

