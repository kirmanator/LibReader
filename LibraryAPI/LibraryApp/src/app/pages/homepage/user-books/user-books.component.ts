import { Component, inject, OnInit, signal } from '@angular/core';
import { BookComponent } from '../../../components/book/book.component';
import { LoanApi } from '../../../apis/loan.api';
import { SessionService } from '../../../services/session.service';
import { of, switchMap, tap } from 'rxjs';
import { LibraryApi } from '../../../apis/library.api';
import { Book } from '../../../models/book.model';
import { LibraryService } from '../../../services/library.service';

@Component({
  selector: 'user-books',
  standalone: true,
  imports: [BookComponent],
  templateUrl: './user-books.component.html',
  styleUrl: './user-books.component.scss'
})
export class UserBooksComponent implements OnInit {

  loanApi: LoanApi = inject(LoanApi);
  libraryApi: LibraryApi = inject(LibraryApi);
  libraryService: LibraryService = inject(LibraryService);
  sessionService: SessionService = inject(SessionService);

  readonly userBooks = this.libraryService.userBooks;
  bookIdToCheckedOutMap: Map<number, Date> = new Map();


  ngOnInit() {
    this.loanApi.getAllByUser(this.sessionService.UserId())
    .pipe(
      tap(bookloans => { 
        bookloans.forEach(bookloan => {
          this.bookIdToCheckedOutMap.set(bookloan.bookId, bookloan.checkedOut);
        });
        if (bookloans.length) {
          this.libraryService.updateUserBooksAsync(bookloans.map(loan => loan.bookId));
        }
      }),
    )
  }
}
