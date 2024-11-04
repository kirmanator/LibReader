export class Book {
    bookId: number;
    title: string;
    description: string;
    author: string;
    publisher: string;
    publicationDate: string;
    genre: string;
    isbn: string;
    pageCount: number;
    available: boolean;

    constructor (
        bookId: number,
        title: string,
        description: string,
        author: string,
        publisher: string,
        publicationDate: string,
        genre: string,
        isbn: string,
        pageCount: number,
        available: boolean) {
            this.bookId = bookId;
            this.title = title;
            this.description = description;
            this.author = author;
            this.publisher = publisher;
            this.publicationDate = publicationDate;
            this.genre = genre;
            this.isbn = isbn;
            this.pageCount = pageCount;
            this.available = available;
    }
}
