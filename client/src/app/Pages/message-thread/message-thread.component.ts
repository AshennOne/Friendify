import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Message } from 'src/app/_models/Message';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-message-thread',
  templateUrl: './message-thread.component.html',
  styleUrls: ['./message-thread.component.css'],
})
export class MessageThreadComponent implements OnInit {
  messages: Message[] = [];
  constructor(
    private route: ActivatedRoute,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      var id = params.get('id');
      if (id)
        this.messageService.getMessageThread(id).subscribe({
          next: (messages) => {
            this.messages = messages;
          },
        });
    });
  }
}
