import { inject, Injectable, signal } from '@angular/core';
import { UserApi } from '../apis/user.api';
import { UserInfo } from '../models/user-info.model';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { CredentialsDto } from '../dtos/credentials-dto.model';
import { SessionService } from './session.service';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class LoginService {

    userApi: UserApi = inject(UserApi);
    sessionService: SessionService = inject(SessionService);
    readonly router: Router = inject(Router);

    failedLogin = signal<boolean>(false);

  getUserById(userId: number): UserInfo[] {
    this.userApi.getById(userId).pipe()
    .subscribe({
      next: user => { 
        console.log("We have a user!");
        console.log(user);
        return user;
      },
      error: err => {console.log("We most assuredly do not have a user! " + err);}
    });
    return [];
  };

  existsByUsername(username: string): boolean {
    this.userApi.existsByUsername(username).pipe()
    .subscribe({
        next: exists => { return exists;},
        error: err => console.log(`Error when checking for username existence: ${err}`)
    });
    return false;
  }

  checkCredentials(username: string, password: string): Observable<UserInfo> {
    return this.userApi.loginWithCredentials(new CredentialsDto(username, password)).pipe(
      catchError(err => {
        console.log("Error caught");
        return throwError(() => "Invalid credentials");
      })
    );
  }

  login(user: UserInfo) {
    this.updateSession(user);
    this.router.navigate(['/homepage']);
  }

  logout(admin: boolean = false, failed: boolean = false) {
    console.log("Logging out");
    this.sessionService.clearSession();
    this.failedLogin.set(failed);
    this.router.navigate(admin? ['/login/admin'] : ['/login']);
  }

  updateFailedLogin(failed: boolean = false) {
    this.failedLogin.set(failed);
  }

  updateSession(user: UserInfo) {
    this.sessionService.updateSession(user);
  }

  createAccount(userInfo: UserInfo): void {
    this.userApi.createAccount(userInfo).pipe()
    .subscribe({
      next: () => { console.log(`User ${userInfo.username} inserted`); },
      error: err => { console.log(`User ${userInfo.username} not inserted! ${err}`); }
    });
  }

  updateAccount(userInfo: UserInfo): void {
    this.userApi.updateById(userInfo).pipe()
    .subscribe({
      next: () => { console.log(`User ${userInfo.username} updated`); },
      error: err => { console.log(`User ${userInfo.username} not updated! ${err}`); }
    });
  }

  deleteAccountById(userId: number): void {
    this.userApi.deleteById(userId).pipe()
    .subscribe({
      next: () => { console.log(`User ${userId} deleted`); },
      error: err => { console.log(`User ${userId} not deleted! ${err}`); }
    });
  }
}
