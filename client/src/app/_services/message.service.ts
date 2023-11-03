import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/Message';
import { MessageBody } from '../_models/MessageBody';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { 
  }
  getMessageHeadlines(){
    return this.http.get<Message[]>(this.baseUrl+'messages/last')
  }
  getMessageThread(id:string){
    return this.http.get<Message[]>(this.baseUrl +"messages/id/"+id)
  }
  sendMessage(id:string, content:string){
    var messageBody:MessageBody = {
      content: content,
      userId: id
    }
    return this.http.post(this.baseUrl+'messages',messageBody)
  }
}
