import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/_models/Post';
import { User } from 'src/app/_models/User';
import { LikeService } from 'src/app/_services/like.service';
import { PostService } from 'src/app/_services/post.service';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
})
export class MainComponent implements OnInit {
  posts: Post[] = [];
  constructor(private postService: PostService) {}

  ngOnInit(): void {
    this.postService.getAllPosts().subscribe({
      next: (posts) => {
        this.posts = posts;
      },
    });
   
  }
  addPostToList(event:any){
    this.posts.unshift(event);
  }
}
