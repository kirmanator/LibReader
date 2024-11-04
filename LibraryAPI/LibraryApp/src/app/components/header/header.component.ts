import { Component, inject, OnInit, signal } from '@angular/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { LoginService } from '../../services/login.service';
import { SessionService } from '../../services/session.service';
import { Role } from '../../models/role.enum';

@Component({
  selector: 'header',
  standalone: true,
  imports: [MatButtonModule, MatMenuModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {

  loginService: LoginService = inject(LoginService);
  sessionService: SessionService = inject(SessionService);
  username = signal<string>('');
  role = signal<string>('');

  ngOnInit() {
    this.username.set(this.sessionService.Username());
    this.role.set(Role[this.sessionService.session().user.roleId - 1])
  }

  logout() {
    this.loginService.logout();
  }
}
