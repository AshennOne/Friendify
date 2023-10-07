import { EventEmitter, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConnectorService {
  unread = new EventEmitter<number>();
  constructor() { }
}
