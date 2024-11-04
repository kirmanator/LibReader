import { Component, Inject, inject, OnInit, signal } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { LibraryApi } from '../../apis/library.api';
import { LoanApi } from '../../apis/loan.api';
import { BookLoan } from '../../models/book-loan.model';
import { SessionService } from '../../services/session.service';
import { environment } from '../../../environments/environment.development';
import { tap } from 'rxjs';

@Component({
  selector: 'app-checkout-dialog',
  standalone: true,
  imports: [MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose, MatButtonModule,],
  templateUrl: './checkout-dialog.component.html',
  styleUrl: './checkout-dialog.component.scss'
})
export class CheckoutDialogComponent implements OnInit {

  readonly bookId: number;
  readonly title: string;

  readonly libraryApi: LibraryApi = inject(LibraryApi);
  readonly loanApi: LoanApi = inject(LoanApi);
  readonly sessionService: SessionService = inject(SessionService);

  readonly dialog = inject(MatDialog);


  constructor(@Inject(MAT_DIALOG_DATA) book: {bookId: number, title: string}) {
    this.bookId = book.bookId;
    this.title = book.title;
  }

  dueDate = signal<string>('');

  ngOnInit() {
    this.updateDueDate();
  }

  updateDueDate() {
    this.dueDate.set(
      this.#addDays(environment.maxCheckout)
      .toLocaleDateString(
          environment.locale, {
        weekday: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric',
      }
    ));
  }

  checkoutBook() {
    this.loanApi.insert(new BookLoan(0, this.sessionService.UserId(), this.bookId, null, null))
      .pipe(
        tap(() => this.libraryApi.setAvailable(this.bookId, false))
      ).subscribe({
        next: (() => { 
          console.log("Successfully checked out");
          this.dialog.closeAll();
        }),
        error: err => console.log(`Could not successfully check ${this.title} out: ${err}`)
      });
  }

  #addDays(days): Date {
    var result = new Date(Date.now());
    result.setDate(result.getDate() + days);
    return result;
  }
}
