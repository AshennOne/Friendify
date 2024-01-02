import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Notification } from 'src/app/_models/Notification';
import { NotificationService } from 'src/app/_services/notification.service';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css'],
})
export class NotificationComponent implements OnInit {
  @Input() notification: Notification = {} as Notification;
  @Output() read = new EventEmitter<boolean>();
  constructor(
    private router: Router,
    private notificationService: NotificationService,
    public presenceService: PresenceService
  ) {}

  ngOnInit(): void {}
  redirect(id: string) {
    if (id == '' || !id) return;
    this.router.navigateByUrl('user/' + id);
  }
  readPost() {
    this.notificationService.readNotifications(this.notification.id).subscribe({
      next: (notification) => {
        this.notification.isRead = true;
        this.read.emit(true);
      },
    });
  }
}
