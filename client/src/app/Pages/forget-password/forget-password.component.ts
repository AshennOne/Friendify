import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css'],
})
export class ForgetPasswordComponent implements OnInit {
  forgetPasswordForm = this.createForm();
  constructor(
    private authService: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {}
  createForm() {
    return new FormGroup(
      {
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [
          Validators.required,
          Validators.minLength(6),
          this.passwordContainsDigitValidator,
          this.passwordContainsUppercaseValidator,
        ]),
        passwordConfirmation: new FormControl('', [Validators.required]),
      },
      { validators: this.passwordMatchValidator }
    );
  }
  onSubmit() {
    this.authService
      .sendForgetPassword(
        this.forgetPasswordForm.controls.email.value + '',
        this.forgetPasswordForm.controls.password.value + ''
      )
      .subscribe({
        next: () => {
          this.toastr.success('Request has been sent on email');
          this.router.navigateByUrl('');
        },
      });
  }
  passwordContainsDigitValidator(
    control: AbstractControl
  ): { [key: string]: boolean } | null {
    var password = control.value;
    if (/\d/.test(password)) {
      return null;
    } else {
      return { passwordWithoutDigit: true };
    }
  }
  passwordMatchValidator(control: AbstractControl) {
    var password = control.get('password')?.value;
    var confirmPassword = control.get('passwordConfirmation')?.value;

    if (password !== confirmPassword) {
      control
        .get('passwordConfirmation')
        ?.setErrors({ passwordMismatch: true });
    } else {
      control.get('passwordConfirmation')?.setErrors(null);
    }

    return null;
  }
  passwordContainsUppercaseValidator(
    control: AbstractControl
  ): { [key: string]: boolean } | null {
    var password = control.value;

    if (/[A-Z]/.test(password)) {
      return null;
    } else {
      return { passwordWithoutUppercase: true };
    }
  }
}
