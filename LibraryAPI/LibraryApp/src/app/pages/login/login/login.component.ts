import { AfterViewInit, Component, ElementRef, inject, ViewChild } from '@angular/core';
import { LoginService } from '../../../services/login.service';
import { FormControl, Validators, ReactiveFormsModule } from '@angular/forms'
import { MatFormFieldModule, MatLabel, MatError } from '@angular/material/form-field'
import { MatInput, MatInputModule } from '@angular/material/input'
import { MatButton, MatButtonModule } from '@angular/material/button'
import { RouterLink } from '@angular/router';

@Component({
  selector: 'login',
  standalone: true,
  imports: [MatLabel, MatError, MatButton, MatButtonModule, ReactiveFormsModule, MatInputModule, MatInput, MatFormFieldModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements AfterViewInit{

  readonly loginService: LoginService = inject(LoginService);
  failedLogin = this.loginService.failedLogin;

  usernameControl = new FormControl<string>('', { validators: Validators.required });
  @ViewChild('username') usernameInput: ElementRef;

  passwordControl = new FormControl<string>('', { validators: Validators.required });
  @ViewChild('password') passwordInput: ElementRef;

  loginWithCredentials() {
    this.loginService.checkCredentials(this.usernameControl.value?.trim() ?? null, this.passwordControl.value?.trim() ?? null)
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
    this.loginService.logout(false, true);
  }

  updateFailedLogin() {
    this.loginService.updateFailedLogin();
  }

  ngAfterViewInit() {
      this.usernameInput.nativeElement.focus();
  }
}
