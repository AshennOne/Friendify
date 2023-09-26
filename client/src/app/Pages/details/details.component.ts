import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/_models/Post';
import { User } from 'src/app/_models/User';
import { PostService } from 'src/app/_services/post.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css'],
})
export class DetailsComponent implements OnInit {
  user: User = {} as User;
  isCurrentUser = false;
  posts: Post[] = [];
  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    private postService: PostService
  ) {}

  ngOnInit() {
    
    this.route.paramMap.subscribe((params) => {
      var id = params.get('id');
      
      if (!id) return;
      this.userService.getUserById(id).subscribe({
        next: (user) => {
          
          this.user = user;
         this.isCurrentUser = this.checkIsCurrent();
          this.getPosts();
        },
        error: (err) => {
          if (err) this.router.navigateByUrl('/main');
        },
      });
    });
  }
  checkIsCurrent() {
    var userString = localStorage.getItem('user');
    if (userString) {
      var parsedUser = JSON.parse(userString) as User;
      if (parsedUser.userName == this.user.userName) {
        return true;
      }
     
    }
      return false;
    
  }
  getPosts() {
    if (!this.user.id) return;
    this.postService.getPostsForUserId(this.user.id).subscribe({
      next: (posts) => {
        this.posts = posts;
      },
    });
  }
}
