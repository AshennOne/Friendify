import { Component, EventEmitter, OnInit, Output, TemplateRef } from '@angular/core';
import {
  Storage,
  ref,
  uploadBytesResumable,
  getDownloadURL,
} from '@angular/fire/storage';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Post } from 'src/app/_models/Post';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css'],
})
export class NewPostComponent implements OnInit {
  user: User = {} as User;
  charCount = 0;
  textContent = '';
  imgUrl = ""
  selectedImageFile?: File;
  option = 'public';
  modalRef?: BsModalRef;
  @Output() newPost = new EventEmitter<Post>();
  constructor(
    private authService: AuthService,
    private modalService: BsModalService,
    private storage: Storage,
    private postService:PostService,
    private toastr:ToastrService
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
  
  loadPhoto(event:any){
    this.selectedImageFile = event;
  }

  onSubmit() {
    if (!this.selectedImageFile) this.addPost();
    else{
      var storageRef = ref(this.storage, 'folder/' + this.selectedImageFile.name);
      var uploadTask = uploadBytesResumable(storageRef, this.selectedImageFile);
      uploadTask.on(
        'state_changed',
        () => {
          
        },
        (error)=>{
          console.log(error)
        },
        () => {
          getDownloadURL(uploadTask.snapshot.ref).then((downloadUrl)=>{
            this.imgUrl = downloadUrl
            this.addPost()
          })
        }
      );
    }
    
  }
  addPost(){
    var post = {
      imgUrl: this.imgUrl,
      textContent: this.textContent
    } as Post;
    this.postService.addPost(post).subscribe({
      next:(post)=>{
        this.toastr.success("Succesfully created new post")
        this.modalRef?.hide();
        this.newPost.emit(post)
        this.textContent = '',
        this.selectedImageFile = {} as File;
      },
      error:(err)=>{
        this.toastr.error(err)
        this.modalRef?.hide()
      }
      
    })
  }
}
