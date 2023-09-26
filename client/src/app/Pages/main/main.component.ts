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
  submittedSearchString= ""
  searchstring = '';
  constructor(private postService: PostService) {}

  ngOnInit(): void {
    this.setSearchString();
    this.getAllPosts();
  }
  addPostToList(event: any) {
    this.getAllPosts();
  }
  setSearchString() {
    var localStorageString = localStorage.getItem('searchstring');
    if (localStorageString) {
      this.searchstring = localStorageString;
    } else {
      this.searchstring = '';
    }
  }
  search() {
    localStorage.setItem('searchstring',this.searchstring)
    this.postService.searchPosts(this.searchstring).subscribe({
      next: (posts) => {
        this.posts = posts;
        this.submittedSearchString = this.searchstring
      },
    });
  }
  reset(){
    this.submittedSearchString = ""
    this.searchstring = ""
    this.getAllPosts()
    localStorage.removeItem('searchstring')
  }
  getAllPosts() {
    if (this.searchstring.length > 0) this.search();
    else {
      this.postService.getAllPosts().subscribe({
        next: (posts) => {
          this.posts = posts;
        },
      });
    }
  }
}
