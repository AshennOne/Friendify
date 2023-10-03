import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Notification } from '../_models/Notification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  apiUrl = environment.apiUrl
  constructor(private http:HttpClient) {
    
   }
   getNotificationsForUser(){
    return this.http.get<Notification[]>(this.apiUrl + 'notifications');
   }
}
