import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { Router } from '@angular/router';
import { Post } from 'src/app/_models/Post';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css'],
})
export class PostComponent implements OnInit, OnChanges {
  @Input() post: Post = {} as Post;
  @Output() onRepostChange = new EventEmitter<Post>();
  @Input() isFromNoti = false;
  user: User = {} as User;
  isLiked = false;
  belongToUser = false;
  isReposted = false;
  constructor(private authService: AuthService,private router:Router) {
    this.authService.getCurrentUser().subscribe({
      next: (user) => {
        this.user = user;
        if (this.post.author?.id == this.user.id) {
          this.belongToUser = true;
        }
      },
    });
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['post'] && this.post && this.user.id) {
      this.checkIsLiked();
      this.checkIsReposted();
      if (this.post.author?.id == this.user.id) {
        this.belongToUser = true;
      }
    }
  }
  checkIsReposted() {
    this.user.reposted?.forEach((element)=>{
      if(this.post && element.id === this.post.id){
        this.isReposted = true
      }
    })
  }
  checkIsLiked() {
    this.user.liked?.forEach((element) => {
      if (this.post && element.id === this.post?.id) {
        this.isLiked = true;
        return;
      }
    });
  }
  ngOnInit(): void {
   
  }
  loadPosts(){
    this.onRepostChange.emit(this.post)
  }
  redirect(id:string){
    if(id == '' || !id) return;
    this.router.navigateByUrl("user/"+id)
  }
}
