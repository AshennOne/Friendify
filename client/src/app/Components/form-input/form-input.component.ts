import { Component, Input, OnInit } from '@angular/core';
import { FormControl,ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-form-input',
  templateUrl: './form-input.component.html',
  styleUrls: ['./form-input.component.css']
})
export class FormInputComponent implements OnInit {
  @Input() control = new FormControl()
  @Input() placeholder:string = ''
   @Input() type:string = 'text'
   @Input() label:string=''
   @Input() iconName:string=''
   eye:string = "fa-eye"
  constructor() { }
  
  ngOnInit(): void {
  }
  hideShowPass()
  {
    if(this.label !== "Password") return;
    if(this.type ==="password"){
      this.type = "text"
      this.eye = "fa-eye-slash"
    }else{
      this.type = "password"
      this.eye = "fa-eye"
    }
  }
}
