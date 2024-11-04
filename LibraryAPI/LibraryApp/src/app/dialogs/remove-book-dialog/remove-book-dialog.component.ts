import { Component, Inject, inject } from '@angular/core';
import { LibraryApi } from '../../apis/library.api';
import { MAT_DIALOG_DATA, MatDialog, MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle } from '@angular/material/dialog';
import { LibraryService } from '../../services/library.service';


@Component({
  selector: 'app-remove-book-dialog',
  standalone: true,
  imports: [MatDialogActions, MatDialogClose, MatDialogContent, MatDialogTitle],
  templateUrl: './remove-book-dialog.component.html',
  styleUrl: './remove-book-dialog.component.scss'
})
export class RemoveBookDialogComponent {
  libraryApi: LibraryApi = inject(LibraryApi);
  libraryService: LibraryService = inject(LibraryService);

  readonly bookId: number;
  readonly title: string;
  readonly dialog = inject(MatDialog);

  constructor(@Inject(MAT_DIALOG_DATA) book: {bookId: number, title: string}) {
    this.bookId = book.bookId;
    this.title = book.title;
  }

  removeBook() {
    this.libraryApi.deleteById(this.bookId)
      .subscribe({
        next: () => { 
          console.log("Deleted " + this.title);
          this.libraryService.updateBooks();
          this.dialog.closeAll();
        },
        error: err => { console.log(`Could not delete ${this.title}:  ${err}`) }
      })
  }
}
