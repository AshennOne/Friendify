import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { ConnectorService } from 'src/app/_services/connector.service';
import { NotificationService } from 'src/app/_services/notification.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  userName = '';
  imgUrl = 'assets/user.png';
  id = '';
  notificationsCount = 0;
  messagesCount = 0;
  constructor(
    private router: Router,
    private toastr: ToastrService,
    private authService: AuthService,
    private connectorService: ConnectorService
  ) {
    this.connectorService.unreadNotifications.subscribe({
      next: (count: number) => {
        this.notificationsCount = count;
      },
    });
    this.connectorService.imgUrl.subscribe({
      next: (imgurl: string) => {
        if (this.imgUrl != '' && this.imgUrl != undefined) {
          this.imgUrl = imgurl;
        } else {
          this.imgUrl = 'assets/user.png';
        }
      },
    });
    this.connectorService.unreadMessages.subscribe({
      next: (count: number) => {
        this.messagesCount = count;
      },
    });
  }

  ngOnInit(): void {
    this.authService.getCurrentUser().subscribe({
      next: (user) => {
        this.id = user.id || '';
        this.userName = user.userName + '';
        if (user.imgUrl) this.imgUrl = user.imgUrl;
      },
    });
  }
  logout() {
    this.authService.logout();
    this.router.navigateByUrl('');
    this.toastr.success('Succesfully logged out');
  }
  redirect() {
    this.router.navigateByUrl('user/' + this.id);
  }
}
