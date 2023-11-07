import { Component, EventEmitter, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
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
  @Input() elementId:number = 0
  @Input() fromUserId = ''
  @Output() isRead = new EventEmitter<boolean>()
  @Input() type?:number;
  modalRef?: BsModalRef;
  post:Post = {} as Post
  constructor(private modalService: BsModalService,private postService:PostService, private router:Router) {}
  ngOnInit(): void {
   
  }
 
  openModal(template: TemplateRef<any>) {
    if(this.elementId)
    this.postService.getPostById(this.elementId).subscribe({
      next:(post)=>{
        this.post = post;
        this.isRead.emit(true);
        this.modalRef = this.modalService.show(template);
      }
    })

   
  }
  redirect(){
    this.router.navigateByUrl('user/'+this.fromUserId)
    this.isRead.emit(true);
  }
  
}
