import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  userName = '';
  imgUrl = '';
  constructor(
    private router: Router,
    private toastr: ToastrService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.getCurrentUser().subscribe({
      next: (user) => {
        
        this.userName = user.userName+"";
        this.imgUrl = user.imgUrl+"";
      },
    });
  }
  logout() {
    localStorage.clear()
    this.router.navigateByUrl('');
    this.toastr.success('Succesfully logged out');
  }
}
