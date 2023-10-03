import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Notification } from 'src/app/_models/Notification';

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.css']
})
export class NotificationComponent implements OnInit {
@Input() notification:Notification = {} as Notification
  constructor(private router:Router) { }

  ngOnInit(): void {
  }
  redirect(id:string){
    if(id == '' || !id) return;
    this.router.navigateByUrl("user/"+id)
  }
}
