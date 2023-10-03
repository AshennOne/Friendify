import { Component, OnInit } from '@angular/core';
import { Notification } from 'src/app/_models/Notification';
import { NotificationService } from 'src/app/_services/notification.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {
notifications:Notification[] = []
  constructor(private notificationService:NotificationService) { 
    this.notificationService.getNotificationsForUser().subscribe({
      next:(notifications)=>{
        this.notifications = notifications;
      }
    })
  }

  ngOnInit(): void {
  }

}
