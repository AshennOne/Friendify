import { Component, Input, OnInit } from '@angular/core';
import { Post } from 'src/app/_models/Post';
import { User } from 'src/app/_models/User';
import { LikeService } from 'src/app/_services/like.service';
import { LocalstorageService } from 'src/app/_services/localstorage.service';

@Component({
  selector: 'app-like',
  templateUrl: './like.component.html',
  styleUrls: ['./like.component.css'],
})
export class LikeComponent implements OnInit {
  @Input() isLiked = false;
  @Input() count = 0
  @Input() post = {} as Post
  @Input() belongToUser = false;
  constructor(private likeService: LikeService,private localStorageService:LocalstorageService) {
    
  }

  ngOnInit(): void {}
  ToggleLike(){
    if(!this.post.id) return;
    if(!this.isLiked){
      this.likeService.AddLike(this.post.id).subscribe({
        next:()=>{
          this.isLiked = true;
          this.count +=1;
          this.localStorageService.addPostToLiked(this.post)
        }
      })
    }else{
      
      this.likeService.RemoveLike(this.post.id).subscribe({
        next:()=>{
          this.isLiked = false;
          this.count -=1;
          this.localStorageService.removePostFromLiked(this.post)
        }
      })
    }
  }
}
