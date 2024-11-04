import { Component, computed, Inject, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialog, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';
import { MatFormFieldModule, MatFormField } from '@angular/material/form-field';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { MatInput, MatInputModule, MatLabel } from '@angular/material/input';
import { NgStyle } from '@angular/common';
import { Book } from '../../models/book.model';
import { SessionService } from '../../services/session.service';
import { LibraryApi } from '../../apis/library.api';
import { CheckoutDialogComponent } from '../checkout-dialog/checkout-dialog.component';
import { RemoveBookDialogComponent } from '../remove-book-dialog/remove-book-dialog.component';
import { UserReview } from '../../models/user-review.model';
import { UserReviewApi } from '../../apis/user-review.api';
import { UserReviewDto } from '../../dtos/user-review-dto.model';

@Component({
  selector: 'app-book-dialog',
  standalone: true,
  imports: [NgStyle, MatFormFieldModule, MatInputModule, ReactiveFormsModule, MatTooltipModule, FormsModule, MatIcon, MatIconModule, MatButtonModule, MatProgressSpinner, MatFormField, MatLabel, MatInput, MatDialogTitle, MatDialogContent, MatDialogActions, MatDialogClose,],
  templateUrl: './book-dialog.component.html',
  styleUrl: './book-dialog.component.scss'
})
export class BookDialogComponent implements OnInit {
  sessionService: SessionService = inject(SessionService);
  libraryApi: LibraryApi = inject(LibraryApi);
  userReviewApi: UserReviewApi = inject(UserReviewApi);
  
  readonly dialog = inject(MatDialog);
  readonly book: Book;
  readonly reviews = signal<UserReviewDto[]>([]);
  averageRating: number;
  stars = signal<string[]>([]);

  isAdmin = signal(false);
  isEditing = signal(false);
  available = signal(false);
  loadingReviews = signal(true);
  private red: number = 122;
  private green: number = 41;
  private blue: number = 23;

  titleFormControl = new FormControl<string>('', { validators: Validators.required });
  authorFormControl = new FormControl<string>('', { validators: Validators.required });
  descriptionFormControl = new FormControl<string>('', { validators: Validators.required });
  publisherFormControl = new FormControl<string>('', { validators: Validators.required });
  publicationDateFormControl = new FormControl<string>('', { validators: Validators.required });
  genreFormControl = new FormControl<string>('', { validators: Validators.required });
  isbnFormControl = new FormControl<string>('', { validators: [Validators.required, Validators.pattern('([A-Z0-9]{10}) || ([A-Z0-9]{13})')] });
  pageCountFormControl = new FormControl<string>('', { validators:Validators.pattern('\\d+')});

  valid = computed(() => this.titleFormControl.valid
    && this.authorFormControl.valid
    && this.descriptionFormControl.valid
    && this.publisherFormControl.valid
    && this.publicationDateFormControl.valid
    && this.genreFormControl.valid
    && this.isbnFormControl.valid
    && this.pageCountFormControl.valid);

  constructor(@Inject(MAT_DIALOG_DATA) data: {book: Book}) {
    this.book = data.book;
  }

  ngOnInit() {
    this.isAdmin.set(this.sessionService.IsAdmin());
    this.libraryApi.isAvailable(this.book.bookId).subscribe(available => this.available.set(available));
    this.titleFormControl = new FormControl<string>(this.book.title, { validators: Validators.required });
    this.authorFormControl = new FormControl<string>(this.book.author, { validators: Validators.required });
    this.descriptionFormControl = new FormControl<string>(this.book.description, { validators: Validators.required });
    this.publisherFormControl = new FormControl<string>(this.book.publisher, { validators: Validators.required });
    this.publicationDateFormControl = new FormControl<string>(this.book.publicationDate, { validators: Validators.required });
    this.genreFormControl = new FormControl<string>(this.book.genre, { validators: Validators.required });
    this.isbnFormControl = new FormControl<string>(this.book.isbn, { validators: [Validators.required, Validators.pattern('([A-Z0-9]{10}) || ([A-Z0-9]{13})')] });
    this.pageCountFormControl = new FormControl<string>(this.book.pageCount.toString(), { validators:Validators.pattern('\\d+')});

    this.userReviewApi.getAllByBook(this.book.bookId)
      .subscribe({
        next: reviews => {
          this.reviews.set(reviews);
          if (reviews.length) {
            this.averageRating = Math.round(reviews.map(r => r.rating).reduce((total, acc) => total + acc) / reviews.length);
            const fullStars = Math.floor(this.averageRating / 2);
            const halfStar = this.averageRating % 2 === 1;
            const emptyStars = 5 - (fullStars + +halfStar);
            const starIcons = [];
            for(var i = 0; i < 5; i++) {
              if (i < fullStars) {
                starIcons.push('star');
              } else if (i === fullStars) {
                starIcons.push(halfStar ? 'star_half':'star_outline');
              } else {
                starIcons.push('star_outline');
              }
            }
            this.stars.set(starIcons);
          }
          this.loadingReviews.set(false);
        },
        error: err => console.log("Unable to retrieve reviews for book " + this.book.bookId)
      });
  }

  startEditing() {
    this.isEditing.set(true);
  }

  stopEditing() {
    this.isEditing.set(false);
  }

  saveEdits() {
    const bookUpdates = new Book(
      this.book.bookId,
      this.titleFormControl.value,
      this.authorFormControl.value,
      this.descriptionFormControl.value,
      this.publisherFormControl.value,
      this.publicationDateFormControl.value,
      this.genreFormControl.value,
      this.isbnFormControl.value,
      parseInt(this.pageCountFormControl.value),
      this.available()
    );

    this.libraryApi.updateById(bookUpdates)
      .subscribe({
        next: () => {
          console.log("Successfully updated book " + this.book.bookId);
        },
        error: err => { console.log(`Could not update book ${this.book.title}: ${err}`)}
      });
  }

  openCheckOutDialog() {
    const dialogRef = this.dialog.open(CheckoutDialogComponent, {
      data: {'bookId': this.book.bookId, 'title': this.book.title},
      width: '80%',
      height: '50%',
      enterAnimationDuration: '50ms',
      exitAnimationDuration: '50ms',
    });
  }

  openRemoveDialog() {
    const dialogRef = this.dialog.open(RemoveBookDialogComponent, {
      data: {'bookId': this.book.bookId, 'title': this.book.title},
      width: '80%',
      height: '50%',
      enterAnimationDuration: '50ms',
      exitAnimationDuration: '50ms',
    });
  }
}
