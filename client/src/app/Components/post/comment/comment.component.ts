import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Comment } from 'src/app/_models/Comment';
import { Post } from 'src/app/_models/Post';
import { CommentService } from 'src/app/_services/comment.service';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css'],
})
export class CommentComponent implements OnInit {
  @Input() count = 0;
  @Input() post = {} as Post;
  @Input() authorId = '';
  textContent = '';
  actuallyEditedComment = '';
  isEditingStageId = 0;
  comments: Comment[] = [];
  modalRef?: BsModalRef;
  constructor(
    private modalService: BsModalService,
    private commentService: CommentService
  ) {}

  ngOnInit(): void {}
  openModal(template: TemplateRef<any>) {
    this.getComments();
    this.modalRef = this.modalService.show(template);
  }
  addComment() {
    if (!this.post.id) return;
    const newComment: Comment = {
      content: this.textContent,
      postId: this.post.id,
    };

    this.commentService.AddComment(newComment).subscribe({
      next: () => {
        this.count += 1;
        this.textContent = '';
        this.getComments();
      },
    });
  }
  getComments() {
    if (!this.post.id) return;
    this.commentService.GetCommentsForPost(this.post.id).subscribe({
      next: (comments) => {
        this.comments = comments;
      },
    });
  }
  onEditComment(comment: Comment) {
    if (this.isEditingStageId !== comment.id) {
      this.actuallyEditedComment = comment.content + '';
      this.isEditingStageId = comment.id || 0;
    } else {
      this.isEditingStageId = 0;
    }
  }
  editComment(comment: Comment) {

    var newComment: Comment = {
      content: this.actuallyEditedComment,
    } as Comment;
    if (!comment.id || comment.id == 0) return;
    this.commentService.EditComment(comment.id, newComment).subscribe({
      next: () => {
        comment.content = this.actuallyEditedComment;
        this.isEditingStageId = 0;
      },
    });
  }
  onDeleteComment(comment: Comment) {
    if(!comment.id) return
    this.commentService.DeleteComment(comment.id).subscribe({
      next:()=>{
        this.count -= 1;
        this.comments= this.comments.filter(c => c.id != comment.id)
      }
    })
  }
}
