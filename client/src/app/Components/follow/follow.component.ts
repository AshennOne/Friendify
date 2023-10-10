import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { User } from 'src/app/_models/User';
import { FollowService } from 'src/app/_services/follow.service';
import { LocalstorageService } from 'src/app/_services/localstorage.service';

@Component({
  selector: 'app-follow',
  templateUrl: './follow.component.html',
  styleUrls: ['./follow.component.css']
})
export class FollowComponent implements OnInit,OnChanges {
  isFollowedByCurrent = false;
  @Input() isCurrentUser = false;
  @Input() currentUserId = ""
  @Input() user:User = {} as User
  @Output() isFollowed = new EventEmitter<boolean>()
  constructor(private followService:FollowService,private localStorageService: LocalstorageService) { }
  ngOnChanges(changes: SimpleChanges): void {
    if((changes['currentUserId'] || changes['user']) && this.user.id && this.currentUserId.length>0){
      this.checkIsFollowed();
    }
  }

  ngOnInit(): void {
    if(this.user.id)
    this.checkIsFollowed();
  }
  toggleFollow() {
    if (this.isFollowedByCurrent) {
      this.unfollow();
    } else {
      this.follow();
    }
  }
  follow() {
    if (this.user.id && !this.isFollowedByCurrent)
      this.followService.follow(this.user.id).subscribe({
        next: (follow) => {
          this.isFollowedByCurrent = true;
          this.localStorageService.addFollow(follow);
          this.isFollowed.emit(true)
        },
      });
  }
  unfollow() {
    if (this.user.id && this.isFollowedByCurrent)
      this.followService.unfollow(this.user.id).subscribe({
        next: () => {
          this.isFollowedByCurrent = false;
          this.localStorageService.removeFollow(this.user.id + '');
          this.isFollowed.emit(false)
        },
      });
  }
  checkIsFollowed() {
    if (this.isCurrentUser == false) {
      this.user.followers?.forEach((element) => {
        if (element.followerId == this.currentUserId) {
          this.isFollowedByCurrent = true;
        } else{
          this.isFollowedByCurrent = false;
        }
      });
    }else{
      this.isFollowedByCurrent = false;
    } 
  
  }
}
