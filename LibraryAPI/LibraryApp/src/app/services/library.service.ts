import { effect, inject, Injectable, signal } from "@angular/core";
import { Book } from "../models/book.model";
import { LibraryApi } from "../apis/library.api";
import { LoanApi } from "../apis/loan.api";
import { SessionService } from "./session.service";
import { map, Observable, switchMap, throwError } from "rxjs";

@Injectable({ providedIn: 'root' })
export class LibraryService {

    libraryApi: LibraryApi = inject(LibraryApi);
    loanApi: LoanApi = inject(LoanApi);
    sessionService: SessionService = inject(SessionService);

    paginatedBooks = signal<Book[]>([]);
    featuredBooks = signal<Book[]>([]);
    userBooks = signal<Book[]>([]);

    private page: number = 0;
    private pageSize: number = 50;
    readonly totalBooks = signal(0);
    readonly defaultPageSize = 10;


    constructor() {
        effect(() => {
            this.updateBooks();
            this.libraryApi.getTotalBooks()
                .subscribe({
                    next: count => this.totalBooks.set(count),
                    error: err => console.log("Could not retrieve total books")
                });
        }, { allowSignalWrites: true });
    }

    updatePaginatedBooksAsync(pageNumber: number = 0, pageSize = this.defaultPageSize, refresh: boolean = false) {
        if (refresh || pageNumber !== this.page || pageSize !== this.pageSize || !this.paginatedBooks.length) {
            this.page = pageNumber;
            this.pageSize = pageSize;
            this.libraryApi.getPaginated(pageSize, pageNumber)
                .subscribe({
                    next: books => {
                        this.paginatedBooks.update(existing => [...books]);
                    },
                    error: err => {console.log("Couldn't retrieve paginated books: " + err)}
                });
        }
    }

    updatePaginatedBooks(books: Book[]) {
        this.paginatedBooks.update(() => [...books]);
    }

    updateFeaturedBooksAsync(refresh: boolean = false) {
        if (refresh || !this.featuredBooks.length) {
            this.libraryApi.getFeatured()
                .subscribe({
                    next: books => {
                        this.featuredBooks.update(existing => [...books]);
                    },
                    error: err => {console.log("Couldn't retrieve featured books: " + err)}
                });
        }
    }

    updateFeaturedBooks(books: Book[]) {
        this.featuredBooks.set(books);
    }

    updateUserBooksAsync(bookIds: number[], refresh: boolean = false) {
        if (refresh || !this.userBooks.length) {
            this.loanApi.getAllByUser(this.sessionService.UserId())
                .pipe(
                    switchMap(loans => {
                        if(!loans.length) {
                            return throwError(() => "No loans exist for this user");
                        }
                        return this.libraryApi.getAllByIds(bookIds);
                    })
                ).subscribe({
                    next: books => this.userBooks.update(existing => [...books]),
                    error: err => console.log("Error retrieving checked out books, user might not have any checked out books")
                }); 
        }
    }

    updateUserBooks(books: Book[]) {
        this.userBooks.update(existing => [...books]);
    }

    updateBooks() {
        this.updateFeaturedBooksAsync(true);
        this.updatePaginatedBooksAsync(this.page, this.pageSize, true);
        this.updateUserBooksAsync(this.userBooks().map(book => book.bookId), true);
    }

    searchByQuery(query: string): Observable<Book[]> {
        return this.libraryApi.searchForSimilarTitles(query);
    }

    // advancedSearchByQuery(query): Observable<Book[]> {
    //     return this.libraryApi.advancedSearchForBooks(query)
    //         .pipe();
    // }
}