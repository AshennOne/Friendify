import { Component, OnInit } from '@angular/core';
import { Notification } from 'src/app/_models/Notification';
import { ConnectorService } from 'src/app/_services/connector.service';
import { NotificationService } from 'src/app/_services/notification.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {
notifications:Notification[] = []

unreadCount = 0;
  constructor(private notificationService:NotificationService, private connectorService:ConnectorService) { 
    this.notificationService.getNotificationsForUser().subscribe({
      next:(notifications)=>{
        this.notifications = notifications;
        var unread = this.notifications.filter(n => n.isRead == false)
        this.unreadCount = unread.length
        this.connectorService.unread.emit(this.unreadCount)
      }
    })
  }

  ngOnInit(): void {
  }
emitCounter(){
  this.unreadCount -= 1
  this.connectorService.unread.emit(this.unreadCount)
}
}
