import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  type:string = "password"
  eye:string = "fa-eye"
  constructor() { }

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
