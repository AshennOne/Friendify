import { Component, Input, OnInit } from '@angular/core';
import { FormControl,ReactiveFormsModule } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

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
   @Input() isLoginForm = false
   eye:string = "fa-eye"
   @Input() isDatePicker = false;
   public bsConfig: Partial<BsDatepickerConfig>;
   constructor() {
    const maxDate = new Date();
    maxDate.setFullYear(maxDate.getFullYear() - 18);

    this.bsConfig = {
      maxDate: maxDate,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-dark-blue'
    };
  }
   
  ngOnInit(): void {
  }
  hideShowPass()
  {
    
    if(this.type ==="password"){
      this.type = "text"
      this.eye = "fa-eye-slash"
    }else{
      this.type = "password"
      this.eye = "fa-eye"
    }
  }
}
