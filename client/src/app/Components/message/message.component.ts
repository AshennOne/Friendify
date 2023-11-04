import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Message } from 'src/app/_models/Message';
import { User } from 'src/app/_models/User';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css']
})
export class MessageComponent implements OnInit, OnChanges {
  @Input() message?:Message
  @Input() isHeadline = false;
  currentUser?:User
  viewedUser? :User
  cropedContent = ""
  constructor(private router:Router) { }
  ngOnChanges(changes: SimpleChanges): void {
  
  }
  redirect(){
    this.router.navigateByUrl('messages/'+this.viewedUser?.id)
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
