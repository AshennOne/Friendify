import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/_models/Post';
import { User } from 'src/app/_models/User';
import { FollowService } from 'src/app/_services/follow.service';
import { LocalstorageService } from 'src/app/_services/localstorage.service';
import { PostService } from 'src/app/_services/post.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css'],
})
export class DetailsComponent implements OnInit {
  user: User = {} as User;

  posts: Post[] = [];
  isCurrentUser = false;
  currentUserId = '';
  followersCount = 0;
  followedCount = 0;
  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    private postService: PostService,
    private followService: FollowService
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      var id = params.get('id');

      if (!id) return;
      this.userService.getUserById(id).subscribe({
        next: (user) => {
          this.user = user;

          this.getPosts();
          this.isCurrentUser = this.checkIsCurrent();
          this.checkFollowedCount();
          this.checkFollowersCount();
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
      if (!parsedUser.id) return false;
      this.currentUserId = parsedUser.id;
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

  checkFollowersCount() {
    if (this.user.id)
      this.followService.getFollowers(this.user.id).subscribe({
        next: (followers) => {
          this.followersCount = followers.length;
        },
      });
  }
  checkFollowedCount() {
    if (this.user.id)
      this.followService.getFollowed(this.user.id).subscribe({
        next: (followed) => {
          this.followedCount = followed.length;
        },
      });
  }
  changeCounter(event:any){
    if(event == false){
      this.followersCount -=1
    }else{
      this.followersCount +=1
    }

  }
}
