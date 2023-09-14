import { Component, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
username = ''
  constructor(private router:Router,private toastr:ToastrService) { }

  ngOnInit(): void {
   var username= localStorage.getItem('username')
   if(username) this.username = username
   else this.username = "user"
   
  }
  logout(){
    localStorage.removeItem('token');
    this.router.navigateByUrl('');
    this.toastr.success("Succesfully logged out");
  }
}
