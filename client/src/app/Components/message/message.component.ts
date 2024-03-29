import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { Router } from '@angular/router';
import { Message } from 'src/app/_models/Message';
import { User } from 'src/app/_models/User';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.css'],
})
export class MessageComponent implements OnInit, OnChanges {
  @Input() message: Message = {} as Message;
  @Input() isHeadline = false;
  currentUser?: User;
  viewedUser: User = {} as User;
  cropedContent = '';
  constructor(
    private router: Router,
    public presenceService: PresenceService
  ) {}
  ngOnChanges(changes: SimpleChanges): void {}
  redirect() {
    this.router.navigateByUrl('messages/' + this.viewedUser?.id);
  }
  redirectToProfile(){
    this.router.navigateByUrl('user/'+this.viewedUser.id)
  }
  ngOnInit(): void {
    this.cropedContent = this.message?.content?.substring(0, 40) + '';
    if (this.message?.content && this.message?.content?.length > 40)
      this.cropedContent += '...';
    var stringUser = localStorage.getItem('user');
    if (stringUser) this.currentUser = JSON.parse(stringUser);
    if (this.currentUser && this.message) {
      if (this.currentUser.id == this.message.senderId) {
        if (this.message.receiver) {
          this.viewedUser = this.message.receiver;
        }
      } else {
        if (this.message.sender) {
          this.viewedUser = this.message.sender;
        }
      }
    }
  }
}
