import { Component, inject, ViewChild } from '@angular/core';
import { FormControl, Validators, ReactiveFormsModule, AsyncValidatorFn } from '@angular/forms';
import { MatFormFieldModule, MatLabel, MatError } from '@angular/material/form-field'
import { MatInput, MatInputModule } from '@angular/material/input';
import { MatButton, MatButtonModule } from '@angular/material/button'
import { UserApi } from '../../../apis/user.api';
import { map, of, switchMap, timer } from 'rxjs';
import { UserInfo } from '../../../models/user-info.model';
import { LoginService } from '../../../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-account',
  standalone: true,
  imports: [ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatLabel, MatError, MatButton, MatButtonModule],
  templateUrl: './create-account.component.html',
  styleUrl: './create-account.component.scss'
})
export class CreateAccountComponent {

  readonly userApi: UserApi = inject(UserApi);
  readonly loginService: LoginService = inject(LoginService);
  readonly router: Router = inject(Router);

  usernameControl = new FormControl<string>('', { validators: Validators.required, asyncValidators: this.usernameExistsValidator() });
  @ViewChild('username') usernameInput: MatInput;

  passwordControl = new FormControl<string>('', { validators: Validators.required });
  @ViewChild('password') passwordInput: MatInput;

  createNewAccount() {
    try {
      this.loginService.createAccount(new UserInfo(0, this.usernameControl.value, this.passwordControl.value, 1));
      this.router.navigate(['/homepage']);
    } catch (e) {
      const result = e.message;
      if (typeof e === 'string') {
        console.log(e.toUpperCase());
      } else if (e instanceof Error) {
        console.log(e.message);
      }
    }

  }

  private usernameExistsValidator(): AsyncValidatorFn {
    return (usernameControl: FormControl) => {
      return timer(300)
        .pipe(
          map(() => usernameControl.value?.trim() ?? null),
          switchMap(username => username ? this.userApi.existsByUsername(username) : of(false)),
          map(exists => !exists ? null : { usernameExists: true})
        );
    };
  }

  valid() {
    return this.usernameControl.valid && this.passwordControl.valid;
  }
}
