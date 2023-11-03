import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Message } from 'src/app/_models/Message';
import { User } from 'src/app/_models/User';

@Component({
  selector: 'app-message-headline',
  templateUrl: './message-headline.component.html',
  styleUrls: ['./message-headline.component.css']
})
export class MessageHeadlineComponent implements OnInit, OnChanges {
  @Input() message?:Message
  currentUser?:User
  viewedUser? :User
  cropedContent = ""
  constructor() { }
  ngOnChanges(changes: SimpleChanges): void {
  
  }

  ngOnInit(): void {
    this.cropedContent = this.message?.content?.substring(0,40) +''
    if(this.message?.content && this.message?.content?.length>40) this.cropedContent+="..."
    var stringUser = localStorage.getItem('user')
    if(stringUser)
    this.currentUser = JSON.parse(stringUser)
    if(this.currentUser && this.message){
      if(this.currentUser.id == this.message.senderId){
       this.viewedUser = this.message.receiver
      }else{
        this.viewedUser = this.message.sender
      }
    }
  }
  
}
