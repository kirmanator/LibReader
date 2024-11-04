import { ChangeDetectionStrategy, Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatTab, MatTabChangeEvent, MatTabContent, MatTabGroup } from '@angular/material/tabs'
import { FeaturedBooksComponent } from '../featured-books/featured-books.component';
import { UserBooksComponent } from '../user-books/user-books.component';
import { BrowseBooksComponent } from '../browse-books/browse-books.component';
import { SharedModule } from '../../../modules/shared/shared.module';
import { SessionService } from '../../../services/session.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrl: './homepage.component.scss',
  standalone: true,
  imports: [SharedModule, MatTab, MatTabContent, MatTabGroup, MatIcon, MatIconModule, FeaturedBooksComponent, UserBooksComponent, BrowseBooksComponent],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomepageComponent implements OnInit{
  sessionService: SessionService = inject(SessionService);
  router: Router = inject(Router);

  @ViewChild('tabGroup') tabs: MatTabGroup;
  currentTab = 0;

  @HostListener('window:keyup', ['$event'])
  onKeyUp(event: KeyboardEvent) {
    if (event.key === 'Tab') {
      this.currentTab = (this.currentTab + 1) % 2;
      this.tabs.selectedIndex = this.currentTab;
    }
  }

  ngOnInit() {
    if (!this.sessionService.session().user) {
      this.router.navigate(["/login"]);
    }
  }
}
