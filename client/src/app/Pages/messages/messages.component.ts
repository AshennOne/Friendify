import { Component, OnInit } from '@angular/core';
import { Message } from 'src/app/_models/Message';
import { ConnectorService } from 'src/app/_services/connector.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css'],
})
export class MessagesComponent implements OnInit {
  lastMessages: Message[] = [];
  unreadCount = 0;
  constructor(
    private messageService: MessageService,
    private connectorService: ConnectorService
  ) {
    this.emitCount();
    this.connectorService.updateMessagesCount.subscribe({
      next: (updated: boolean) => {
        if (updated) {
          this.emitCount();
        }
      },
    });
  }

  ngOnInit(): void {}
  emitCount() {
    this.messageService.getMessageHeadlines().subscribe({
      next: (messages) => {
        this.lastMessages = messages;
        this.unreadCount = messages.filter((m) => m.read == false).length;
        this.connectorService.unreadMessages.emit(this.unreadCount);
      },
    });
  }
}
