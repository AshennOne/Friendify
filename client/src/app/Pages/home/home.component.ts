import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserAuth } from 'src/app/_models/UserAuth';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  loginForm = this.createForm();

  constructor(
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  createForm() {
    return new FormGroup({
      emailOrUsername: new FormControl('', [
        Validators.required,
        Validators.minLength(4),
      ]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(6),
        this.passwordContainsDigitValidator,
      ]),
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.authService.login(this.getUserFromForm()).subscribe({
        next: (user) => {
          if (!user) this.toastr.error('user not found');
          localStorage.setItem('token', user.token + '');
          localStorage.setItem('username', user.userName + '');
          this.toastr.success('Succesfully logged in');
          this.router.navigateByUrl('main');
        },
        error: (err) => {
          if (err.error === 'Email needs to be confirmed') {
            this.router.navigateByUrl('confirmemail');
          } else {
            this.toastr.error(err.error);
          }
        },
      });
    }
  }
  passwordContainsDigitValidator(
    control: AbstractControl
  ): { [key: string]: boolean } | null {
    const password = control.value;
    if (/\d/.test(password)) {
      return null;
    } else {
      return { passwordWithoutDigit: true };
    }
  }

  private getUserFromForm() {
    if (this.loginForm.valid) {
      return {
        userNameOrEmail: this.loginForm.get('emailOrUsername')?.value,
        password: this.loginForm.get('password')?.value,
      } as UserAuth;
    }
    return {} as UserAuth;
  }
}
