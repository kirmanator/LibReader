import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject, OnInit, signal } from '@angular/core';
import { LibraryApi } from '../../../apis/library.api';
import { BookComponent } from '../../../components/book/book.component';
import { MatInput, MatInputModule, MatLabel } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { SearchComponent } from '../../../components/search/search.component';
import { LibraryService } from '../../../services/library.service';
import { FormControl } from '@angular/forms';
import { Book } from '../../../models/book.model';
import { BookDialogComponent } from '../../../dialogs/book-dialog/book-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'browse-books',
  standalone: true,
  imports: [BookComponent, SearchComponent, MatInput, MatLabel, MatInputModule, MatButtonModule, MatSelectModule, ReactiveFormsModule, MatFormField, MatFormFieldModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './browse-books.component.html', 
  styleUrl: './browse-books.component.scss'
})
export class BrowseBooksComponent implements OnInit {

  libraryApi: LibraryApi = inject(LibraryApi);
  libraryService: LibraryService = inject(LibraryService);

  readonly dialog = inject(MatDialog);
  readonly paginatedBooks = this.libraryService.paginatedBooks;
  readonly loading = signal(true);

  page = signal(0);
  pageSizeControl = new FormControl<number>(this.libraryService.defaultPageSize);
  totalPages = signal(0);

  constructor(private cdRef: ChangeDetectorRef) {

  }

  ngOnInit() {
    this.totalPages.set(this.libraryService.totalBooks() > 0 
      ? (this.libraryService.totalBooks()/this.pageSizeControl.value) + (this.libraryService.totalBooks() % this.pageSizeControl.value > 0 ? 1 : 0)
      : 0);
  }

  previousPage() {
    if (this.page() > 0) {
      this.page.set(this.page() - 1);
      this.updatePage();
    }
  }

  nextPage() {
    if (this.page() < this.totalPages()) {
      this.page.set(this.page() + 1);
      this.updatePage();
    }
  }

  private updatePage() {
    this.libraryService.updatePaginatedBooksAsync(this.page(), this.pageSizeControl.value);
    this.cdRef.detectChanges();
  }

  openBookDialog(book: Book, enterAnimationDuration: string, exitAnimationDuration: string): void {
    const dialogRef = this.dialog.open(BookDialogComponent, {
      data: {book: book},
      width: '80%',
      height: '80%',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }
}
