import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Comment } from 'src/app/_models/Comment';
import { Post } from 'src/app/_models/Post';
import { CommentService } from 'src/app/_services/comment.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() count = 0
  @Input() post = {} as Post
  textContent = ""
  comments:Comment[] = []
  modalRef?: BsModalRef;
  constructor( private modalService: BsModalService, private commentService:CommentService) { }

  ngOnInit(): void {
  }
  openModal(template: TemplateRef<any>) {
   
    this.getComments()
    this.modalRef = this.modalService.show(template);
    
  }
  addComment(){
    if(!this.post.id) return;
    const newComment: Comment = {
      content: this.textContent,
      postId: this.post.id,
    };
   
    this.commentService.AddComment(newComment).subscribe({
      next:()=>{
        this.count+=1
        this.textContent = ""
        this.getComments()
      }
    })
  }
  getComments(){
    if(!this.post.id) return;
    this.commentService.GetCommentsForPost(this.post.id).subscribe({
      next:(comments)=>{
        this.comments = comments
        
      }
    })
  }
}
