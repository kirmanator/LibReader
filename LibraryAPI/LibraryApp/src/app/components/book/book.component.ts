import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit, signal } from '@angular/core';
import { MatProgressSpinner } from '@angular/material/progress-spinner';
import { NgStyle } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIcon, MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'book', 
  standalone: true,
  imports: [MatProgressSpinner, NgStyle, MatTooltipModule, MatIcon, MatIconModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './book.component.html',
  styleUrl: './book.component.scss'
})
export class BookComponent implements OnInit{

  imageSourceBase: string = `https://covers.openlibrary.org/b/isbn/`;

  @Input() isbn: string;
  imageSource = signal<string>('');

  @Input() availability: boolean;
  available = signal(false);

  loading = signal(true);
  errorLoad = signal(false);

  ngOnInit() {
    this.imageSource.update(() => `${this.imageSourceBase}${this.isbn}-L.jpg`);
    this.available.set(this.availability);
  }

  hideLoader() {
    this.loading.set(false);
  }

  error() {
    this.loading.set(true);
  }
}
