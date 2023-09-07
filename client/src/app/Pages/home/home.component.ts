import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  loginForm = this.createForm()

  constructor() {
    
  }


  createForm(){
    return  new FormGroup({
      emailOrUsername: new FormControl('',[Validators.required, Validators.minLength(4),this.usernameCharacterValidator]),
      password: new FormControl('', [Validators.required, Validators.minLength(6), this.passwordContainsDigitValidator])
    });
  }
  
  onSubmit() {
    if (this.loginForm.valid) {
      console.log( this.loginForm.value);
    }
  }
  passwordContainsDigitValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const password = control.value;
    if (/\d/.test(password)) {
      return null; 
    } else {
      return { 'passwordWithoutDigit': true }; 
    }
  }
  usernameCharacterValidator(control: AbstractControl): { [key: string]: boolean } | null {
    const username = control.value;
    const regex = /^[a-zA-Z0-9_.-]*$/; 

    if (regex.test(username)) {
      
      return null; 
    } else {
      
      return { 'invalidUsernameCharacters': true }; 
    }
  }
}
