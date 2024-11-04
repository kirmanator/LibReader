import { Component, inject, OnInit, signal } from '@angular/core';
import { Book } from '../../../models/book.model';
import { LibraryApi } from '../../../apis/library.api';
import { BookComponent } from '../../../components/book/book.component';
import { MatDialog } from '@angular/material/dialog';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { BookDialogComponent } from '../../../dialogs/book-dialog/book-dialog.component';
import { LibraryService } from '../../../services/library.service';
import { UserReviewApi } from '../../../apis/user-review.api';

@Component({
  selector: 'featured-books',
  standalone: true,
  imports: [BookComponent, MatProgressSpinner],
  templateUrl: './featured-books.component.html',
  styleUrl: './featured-books.component.scss'
})
export class FeaturedBooksComponent {

  libraryApi: LibraryApi = inject(LibraryApi);
  libraryService: LibraryService = inject(LibraryService);
  userReviewApi: UserReviewApi = inject(UserReviewApi);

  readonly featuredBooks = this.libraryService.featuredBooks;
  readonly featuredRatings = signal<number[]>([]);
  readonly loading = signal(true);

  readonly dialog = inject(MatDialog);

  openBookDialog(book: Book, enterAnimationDuration: string, exitAnimationDuration: string): void {
    const dialogRef = this.dialog.open(BookDialogComponent, {
      data: {book: book},
      width: '80%',
      height: '100%',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }
}
