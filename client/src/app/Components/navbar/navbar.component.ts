import { Component, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { User } from 'firebase/auth';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
userName = ""
imgUrl = ""
  constructor(private router:Router,private toastr:ToastrService) { }

  ngOnInit(): void {
    var userString = localStorage.getItem('user') +''
   var user= JSON.parse(userString) 
   if(!user) return;
   this.userName = user.userName;
   this.imgUrl = user.imgUrl;
  
   
  }
  logout(){
    localStorage.removeItem('token');
    this.router.navigateByUrl('');
    this.toastr.success("Succesfully logged out");
  }
}
