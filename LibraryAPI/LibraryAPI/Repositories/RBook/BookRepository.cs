using Dapper;
using LibraryAPI.Config;
using LibraryAPI.Models;
using Microsoft.Data.SqlClient;
using System.Web.Http;

namespace LibraryAPI.Repositories.RBook
{
    public class BookRepository : IBookRepository
    {
        private readonly ConnectionContext context;
        public BookRepository(ConnectionContext context)
        {
            this.context = context;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {

                string query = """
                SELECT * FROM book
                WHERE book_id = @id
                """;
                Book? book = await connection.QueryFirstOrDefaultAsync<Book>(query, new
                {
                    id
                });
                return book;
            }
        }
        public async Task<bool> IsAvailableAsync(int id) {
            using (var connection = context.CreateConnection()) {
                string query = """
                SELECT available FROM book WHERE book_id = @id
                """;
                bool isAvailable = await connection.QueryFirstAsync<bool>(query, new { id });
                return isAvailable;
            }
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                SELECT 
                    CASE WHEN EXISTS( SELECT 1 FROM book WHERE book_id = @id ) THEN 1
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

        public async Task<List<Book>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                SELECT * FROM book
                """;
                IEnumerable<Book> books = await connection.QueryAsync<Book>(query);
                return books.ToList();
            }
        }

        public async Task<List<Book>> GetAllAsync(List<int> bookIds) {
            using (var connection = context.CreateConnection()) {
                string query = string.Format("""
                SELECT * FROM book
                WHERE book_id IN ({0})
                """,string.Join(",", bookIds.ToArray()));
                IEnumerable<Book> books = await connection.QueryAsync<Book>(query);
                return books.ToList();
            }
        }

        public async Task<List<Book>> GetPaginatedAsync(int pageSize, int pageNumber) {
            using (var connection = context.CreateConnection()) {
                string query = """
                SELECT * FROM book
                ORDER BY book_id
                OFFSET @offset ROWS
                FETCH NEXT @limit ROWS ONLY
                """;
                IEnumerable<Book> books = await connection.QueryAsync<Book>(query, new {limit=pageSize, offset=pageNumber*pageSize});
                return books.ToList();
            }
        }

        // V2: Base featured on number of loans, either show very popular books for that week/month or ones that have been scarcely checked out
        public async Task<List<Book>> GetFeaturedAsync() {
            using (var connection = context.CreateConnection()) {
                string query = """
                SELECT TOP 5 * FROM book
                ORDER BY NEWID()
                """;
                IEnumerable<Book> books = await connection.QueryAsync<Book>(query);
                return books.ToList();
            }
        }

        public async Task<int> GetTotal() {
            using (var connection = context.CreateConnection()) {
                string query = """
                SELECT COUNT(*) FROM book
                """;
                int total = await connection.QueryFirstAsync<int>(query);
                return total;
            }
        }

        public async Task<int> InsertAsync(Book entity)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                INSERT INTO book (title, description, author, publisher, publication_date, genre, isbn, page_count)
                VALUES (@Title, @Description, @Author, @Publisher, @PublicationDate, @Genre, @Isbn, @PageCount)
                """;
                return await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task<List<Book>> SearchAsync(string searchQuery) {
            using (var connection = context.CreateConnection()) {
                string query = String.Format("""
                    SELECT * FROM book
                    WHERE LOWER(title) LIKE '{0}%'
                    ORDER by title
                """, searchQuery);
                IEnumerable<Book> books = await connection.QueryAsync<Book>(query);
                return books.ToList();
            }
        }

        public async Task<int> UpdateAsync(Book entity)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                UPDATE book
                SET title = @Title,
                description = @Description,
                author = @Author,
                publisher = @Publisher,
                publication_date = @PublicationDate,
                genre = @Genre,
                isbn = @Isbn,
                page_count = @PageCount,
                available = @Available
                WHERE book_id = @BookId
                """;
                return await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task SetAvailableAsync(int id, bool available) {
            using (var connection = context.CreateConnection()) {
                string query = """
                UPDATE book
                SET available = @available
                WHERE book_id = @id
                """;
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = context.CreateConnection())
            {
                string query = """
                DELETE FROM book WHERE book_id = @id
                """;
                return await connection.ExecuteAsync(query, new
                {
                    id
                });
            }
        }

    }
}

