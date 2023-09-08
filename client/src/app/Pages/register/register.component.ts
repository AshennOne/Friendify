import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { UserAuth } from 'src/app/_models/UserAuth';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  registerForm = this.createForm();
  constructor(private router:Router,private authService:AuthService) {}

  ngOnInit(): void {}
  createForm() {
    return new FormGroup(
      {
        firstname: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
          this.nameCharacterValidator,
        ]),
        lastname: new FormControl('', [
          Validators.required,
          Validators.minLength(2),
          this.nameCharacterValidator,
        ]),
        username: new FormControl('', [
          Validators.required,
          Validators.minLength(4),
          this.usernameCharacterValidator,
        ]),
        email: new FormControl('', [
          Validators.required,
          Validators.minLength(4),
          Validators.email,
        ]),
        password: new FormControl('', [
          Validators.required,
          Validators.minLength(6),
          this.passwordContainsDigitValidator,
          this.passwordContainsUppercaseValidator,
        ]),
        passwordConfirmation: new FormControl('', [Validators.required]),
        gender: new FormControl('male', [Validators.required]),
        dateOfBirth: new FormControl('', [
          Validators.required,
          this.minimumAgeValidator(18),
        ]),
      },
      { validators: this.passwordMatchValidator }
    );
  }
  onSubmit() {
    this.router.navigateByUrl('confirmemail')
    if (this.registerForm.valid) {
      console.log(this.registerForm.value);
      var user = this.getUserFromForm();
      this.authService.register(user).subscribe({
        next:()=>{
          localStorage.setItem('email',this.registerForm.get('email')?.value+'')
          this.router.navigateByUrl('confirmemail')
        }
      })
    }
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
  usernameCharacterValidator(
    control: AbstractControl
  ): { [key: string]: boolean } | null {
    var username = control.value;
    var regex = /^[a-zA-Z0-9_.-]*$/;

    if (regex.test(username)) {
      return null;
    } else {
      return { invalidUsernameCharacters: true };
    }
  }
  nameCharacterValidator(
    control: AbstractControl
  ): { [key: string]: boolean } | null {
    var name = control.value;
    var regex = /^[a-zA-Z0-9]*$/;

    if (regex.test(name)) {
      return null;
    } else {
      return { invalidNameCharacters: true };
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
  minimumAgeValidator(minAge: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.value) {
        return null;
      }

      var birthDate = new Date(control.value);
      var today = new Date();
      var age = today.getFullYear() - birthDate.getFullYear();

      if (
        today.getMonth() < birthDate.getMonth() ||
        (today.getMonth() === birthDate.getMonth() &&
          today.getDate() < birthDate.getDate())
      ) {
        age--;
      }

      if (age < minAge) {
        return { minimumAge: { requiredAge: minAge, actualAge: age } };
      }

      return null;
    };
  }
  getUserFromForm() {
    if (this.registerForm.valid) {
      return {
        firstname: this.registerForm.get('firstname')?.value,
        lastname: this.registerForm.get('lastname')?.value,
        username: this.registerForm.get('username')?.value,
        email: this.registerForm.get('email')?.value,
        password: this.registerForm.get('password')?.value,
        gender: this.registerForm.get('gender')?.value,
        dateOfBirth: this.registerForm.get('dateOfBirth')?.value,
      } as UserAuth;
    }
    return {} as UserAuth;
  }
}
