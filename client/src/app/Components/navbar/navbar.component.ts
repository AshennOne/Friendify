import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
username = ''
  constructor() { }

  ngOnInit(): void {
   var username= localStorage.getItem('username')
   if(username) this.username = username
   else this.username = "user"
  }

}
