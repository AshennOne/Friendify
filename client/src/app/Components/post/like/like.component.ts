import { Component, Input, OnInit } from '@angular/core';
import { Post } from 'src/app/_models/Post';
import { User } from 'src/app/_models/User';
import { LikeService } from 'src/app/_services/like.service';

@Component({
  selector: 'app-like',
  templateUrl: './like.component.html',
  styleUrls: ['./like.component.css'],
})
export class LikeComponent implements OnInit {
  @Input() isLiked = false;
  @Input() count = 0
  constructor(private likeService: LikeService) {
    
  }

  ngOnInit(): void {}
}
