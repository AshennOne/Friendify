import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Post } from 'src/app/_models/Post';
import { NotificationService } from 'src/app/_services/notification.service';
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'app-post-modal',
  templateUrl: './post-modal.component.html',
  styleUrls: ['./post-modal.component.css']
})
export class PostModalComponent implements OnInit {
  @Input() postId:number = {} as number
  @Output() isRead = new EventEmitter<boolean>()
  modalRef?: BsModalRef;
  post:Post = {} as Post
  constructor(private modalService: BsModalService,private postService:PostService) {}
  ngOnInit(): void {
   
  }
 
  openModal(template: TemplateRef<any>) {
    this.postService.getPostById(this.postId).subscribe({
      next:(post)=>{
        this.post = post;
        this.isRead.emit(true);
        this.modalRef = this.modalService.show(template);
      }
    })

   
  }

}
