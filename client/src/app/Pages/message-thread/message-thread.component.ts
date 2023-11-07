import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Message } from 'src/app/_models/Message';
import { User } from 'src/app/_models/User';
import { ConnectorService } from 'src/app/_services/connector.service';
import { MessageService } from 'src/app/_services/message.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-message-thread',
  templateUrl: './message-thread.component.html',
  styleUrls: ['./message-thread.component.css'],
})
export class MessageThreadComponent implements OnInit {
  @ViewChild('messageArea') messageArea?: ElementRef;
  messages: Message[] = [];
  viewedUser?: User;
  messageContent = ''
  constructor(
    private route: ActivatedRoute,
    private messageService: MessageService,
    private userService: UserService,
    private connectorService:ConnectorService
  ) {
    
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      var id = params.get('id');
      if (!id) return;
      this.messageService.getMessageThread(id).subscribe({
        next: (messages) => {
          this.messages = messages;
          this.connectorService.updateMessagesCount.emit(true)
          if (id)
            this.userService.getUserById(id).subscribe({
              next: (user) => {
              this.viewedUser = user;
              
              this.scrollToBottom()
              
              },
            });
        },
      });
    });
  }
 
  sendMessage(){
    if(!this.viewedUser?.id) return;
    this.messageService.sendMessage(this.viewedUser.id,this.messageContent).subscribe({
      next:(message)=>{
        this.messages.push(message)
        
      }
    })
    this.messageContent = ''
    setTimeout(()=>{
      this.scrollToBottom()
    },100)
    
  }
  scrollToBottom() {
    if(this.messageArea){
      const messageAreaElement: HTMLElement = this.messageArea.nativeElement;
      messageAreaElement.scrollTop = messageAreaElement.scrollHeight;
    }
    
  }
}
