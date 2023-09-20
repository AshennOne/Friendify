import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Post } from 'src/app/_models/Post';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit,OnChanges{
  @Input() post:Post = {} as Post
  user:User = {} as User
  isLiked = false;
  belongToUser = false;
  constructor(private authService:AuthService) {
    this.authService.getCurrentUser().subscribe({
      next:(user)=>{
        this.user = user
      }
    
    })
   }
  ngOnChanges(changes: SimpleChanges): void {
    if(changes['post']&&this.post){
      this.checkIsLiked()
      if(this.post.author?.id==this.user.id){
        this.belongToUser = true
      }
    }
  }
   checkIsLiked(){
    this.user.liked?.forEach(element =>{
      if(this.post&&element.id === this.post?.id){
        this.isLiked = true;
        return;
      }
    })
   }
  ngOnInit(): void {

  }
  
}
