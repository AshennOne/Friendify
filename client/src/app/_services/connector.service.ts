import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConnectorService {
  unreadNotifications = new EventEmitter<number>();
  unreadMessages = new EventEmitter<number>();
  updateMessagesCount = new EventEmitter<boolean>();
  imgUrl = new EventEmitter<string>();
  constructor() { }
}
