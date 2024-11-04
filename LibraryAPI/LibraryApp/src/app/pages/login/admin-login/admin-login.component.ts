import { Component, ElementRef, inject, ViewChild } from '@angular/core';
import { LoginService } from '../../../services/login.service';
import { FormControl, Validators, AsyncValidatorFn, ReactiveFormsModule } from '@angular/forms'
import { MatFormFieldModule, MatLabel, MatError } from '@angular/material/form-field'
import { MatInputModule, MatInput } from '@angular/material/input'
import { MatButton, MatButtonModule } from '@angular/material/button'
import { map, of, switchMap, tap, throwError, timer } from 'rxjs';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'admin-login',
  standalone: true,
  imports: [MatLabel, MatButton, MatError, MatButtonModule, ReactiveFormsModule, MatFormFieldModule, MatInput, MatInputModule, RouterLink],
  templateUrl: './admin-login.component.html',
  styleUrl: './admin-login.component.scss'
})
export class AdminLoginComponent {

  readonly loginService: LoginService = inject(LoginService);
  failedLogin = this.loginService.failedLogin;

  usernameControl = new FormControl<string>('', { validators: Validators.required });
  @ViewChild('username') usernameInput: ElementRef;

  passwordControl = new FormControl<string>('', { validators: Validators.required });
  @ViewChild('password') passwordInput: ElementRef;

  loginWithCredentials() {
    this.loginService.checkCredentials(this.usernameControl.value?.trim() ?? null, this.passwordControl.value?.trim() ?? null)
    .pipe(
      map(user => {
        if (user.roleId == 1) {
          throw new Error("User is not a librarian")
        }
        return user
      })
    )
    .subscribe({
      next: user => this.loginService.login(user),
      error: () => this.resetLogin()
    });
  }

  valid() {
    return this.usernameControl.valid && this.passwordControl.valid;
  }

  clearInput() {
    this.usernameControl.setValue('');
    this.passwordControl.setValue('');
  }

  resetLogin() {
    this.clearInput();
    this.loginService.logout(true, true);
  }

  updateFailedLogin() {
    this.loginService.updateFailedLogin();
  }

  ngAfterViewInit() {
      this.usernameInput.nativeElement.focus();
  }
}
