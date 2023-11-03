import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/_models/Message';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  lastMessages:Message[] = []
  constructor(private messageService:MessageService) {
    this.messageService.getMessageHeadlines().subscribe({
      next:(messages)=>{
        this.lastMessages = messages
      }
    })
   }

  ngOnInit(): void {
  }

}
