using Dapper.FluentMap.Mapping;

namespace LibraryAPI.Models {
    public class Book {
        public int BookId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public string? PublicationDate { get; set; }
        public string? Genre { get; set; }
        public string? Isbn { get; set; }
        public int? PageCount { get; set; }
        public bool? Available { get; set; }

        public Book() {
        }

        public Book(int bookId, string title, string description, string author, string publisher, string publicationDate, string genre, string isbn, int pageCount) {
            BookId = bookId;
            Title = title;
            Description = description;
            Author = author;
            Publisher = publisher;
            PublicationDate = publicationDate;
            Genre = genre;
            Isbn = isbn;
            PageCount = pageCount;
            Available = true;
        }
    }

    internal class BookMap : EntityMap<Book> {
        internal BookMap() {
            Map(entity => entity.BookId).ToColumn("book_id");
            Map(entity => entity.Title).ToColumn("title");
            Map(entity => entity.Description).ToColumn("description");
            Map(entity => entity.Author).ToColumn("author");
            Map(entity => entity.Publisher).ToColumn("publisher");
            Map(entity => entity.PublicationDate).ToColumn("publication_date");
            Map(entity => entity.Genre).ToColumn("genre");
            Map(entity => entity.Isbn).ToColumn("isbn");
            Map(entity => entity.PageCount).ToColumn("page_count");
            Map(entity => entity.Available).ToColumn("available");
        }
    }
}
