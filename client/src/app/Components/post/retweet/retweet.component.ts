import { Component, OnInit, Input, EventEmitter, Output, OnChanges, SimpleChanges } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Post } from 'src/app/_models/Post';
import { LocalstorageService } from 'src/app/_services/localstorage.service';
import { RepostService } from 'src/app/_services/repost.service';

@Component({
  selector: 'app-retweet',
  templateUrl: './retweet.component.html',
  styleUrls: ['./retweet.component.css'],
})
export class RetweetComponent implements OnInit {
  @Input() belongToUser = false;
  @Input() isReposted = false;
  @Input() post = {} as Post;
  @Input() count = 0;
  @Output() onRepostChange = new EventEmitter<Post>();
  constructor(
    private repostService: RepostService,
    private localStorageService: LocalstorageService,
    private toastr: ToastrService
  ) {}
 

  ngOnInit(): void {}
  repost() {
    if (!this.post.id) return;
    if (this.isReposted) {
      this.repostService.unRepost(this.post?.id).subscribe({
        next: () => {
          this.isReposted = false;
          this.count -= 1;
          this.localStorageService.removePostFromReposted(this.post);
          this.onRepostChange.emit(this.post);
        },
        error: (err) => {
          this.toastr.error("You can't repost your own post!");
        },
      });
    } else {
      this.repostService.repost(this.post.id).subscribe({
        next: () => {
          this.isReposted = true;
          this.count += 1;
          this.localStorageService.addPostToReposted(this.post);
          this.onRepostChange.emit(this.post);
        },
        error: (err) => {
          this.toastr.error("You can't repost your own post!");
        },
      });
    }
  }
}
