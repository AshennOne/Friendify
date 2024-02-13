import {
  Component,
  EventEmitter,
  OnInit,
  Output,
  TemplateRef,
} from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Post } from 'src/app/_models/Post';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { PostService } from 'src/app/_services/post.service';
import { UploadService } from 'src/app/_services/upload.service';

@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css'],
})
export class NewPostComponent implements OnInit {
  user: User = {} as User;
  charCount = 0;
  textContent = '';
  imgUrl = '';
  selectedImageFile?: File;
  option = 'public';
  modalRef?: BsModalRef;
  isCropping = false;
  @Output() newPost = new EventEmitter<Post>();
  constructor(
    private authService: AuthService,
    private modalService: BsModalService,
    private uploadService: UploadService,
    private postService: PostService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.authService.getCurrentUser().subscribe({
      next: (user) => {
        this.user = user;
      },
    });
  }
  checkCharCount() {
    this.charCount = this.textContent.length;
  }
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  checkIsCropping(event: any) {
    if (event) this.isCropping = true;
    else this.isCropping = false;
  }
  loadPhoto(event: any) {
    if (event != null) {
      this.selectedImageFile = event;
    }
  }
  hideModal() {
    this.modalService.hide();
    this.selectedImageFile = undefined;
    this.isCropping = false;
  }
  onSubmit() {
    if (this.isCropping) {
      alert('You have uncropped image! remove image or crop it first');
      return;
    }
    if (!this.selectedImageFile) this.addPost();
    else {
      this.uploadService.uploadImage(this.selectedImageFile).subscribe({
        next: (imgUrl) => {
          this.imgUrl = imgUrl.imageUrl;
          this.addPost();
        },
      });
    }
  }
  addPost() {
    var post = {
      imgUrl: this.imgUrl,
      textContent: this.textContent,
    } as Post;
    this.postService.addPost(post).subscribe({
      next: (post) => {
        this.toastr.success('Succesfully created new post');
        this.modalRef?.hide();
        this.newPost.emit(post);
        (this.textContent = ''), (this.selectedImageFile = {} as File);
      },
      error: (err) => {
        this.toastr.error(err);
        this.modalRef?.hide();
      },
    });
  }
}
