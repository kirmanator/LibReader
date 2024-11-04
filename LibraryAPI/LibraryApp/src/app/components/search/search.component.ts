import { Component, inject } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MatInput, MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatLabel, MatFormFieldModule, MatFormField } from '@angular/material/form-field';
import { LibraryService } from '../../services/library.service';

@Component({
  selector: 'search',
  standalone: true,
  imports: [MatInput, MatInputModule, MatLabel, MatIcon, MatIconModule, MatButtonModule, MatFormField, MatFormFieldModule, ReactiveFormsModule, FormsModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent {
  libraryService: LibraryService = inject(LibraryService);
  searchValue: string;

  executeSearch() {
    if (this.searchValue === '') {
      return;
    }
    this.libraryService.searchByQuery(this.searchValue)
      .subscribe({
        next: books => {
          this.searchValue = '';
          this.libraryService.updatePaginatedBooks(books);
        },
        error: err => console.log("Couldn't search for query " + this.searchValue)
      })
  }

  resetSearch() {
    this.libraryService.updatePaginatedBooksAsync(0, this.libraryService.defaultPageSize, true);
  }
}
