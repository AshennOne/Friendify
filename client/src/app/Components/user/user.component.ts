import {
  Component,
  Input,
  OnChanges,
  OnInit,
  SimpleChanges,
} from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/_models/User';
import { PresenceService } from 'src/app/_services/presence.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
})
export class UserComponent implements OnInit,OnChanges{
  @Input() user: User = {} as User;
  currentUser?:User
  currentUserId = ''
  isCurrentUser = false;
  constructor(private router:Router,public presenceService:PresenceService) {}
  ngOnChanges(changes: SimpleChanges): void {
    if(changes['user']&&this.user.id){
      if(this.user.id == this.currentUser?.id){
        this.isCurrentUser = true;
      }else{
        this.isCurrentUser = false;
      }

    }
  }
  ngOnInit(): void {
   var userstring = localStorage.getItem('user')
   if(userstring)
   this.currentUser = JSON.parse(userstring);
  if(!this.currentUser?.id) return;
  this.currentUserId = this.currentUser.id
  }
  
  redirectUser(){
    if(this.user?.id)
    this.router.navigateByUrl('user/'+this.user.id)
  }
  redirectMessages(){
    if(this.user.id){
      this.router.navigateByUrl('messages/'+ this.user.id)
    }
  }
}
