import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-new-post',
  templateUrl: './new-post.component.html',
  styleUrls: ['./new-post.component.css'],
})
export class NewPostComponent implements OnInit {
  user: User = {} as User;
  charCount = 0;
  textContent = '';
  selectedImageFile?:File
  option = 'public';
  modalRef?: BsModalRef;
  constructor(
    private authService: AuthService,
    private modalService: BsModalService
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
  onPhotoSelected(photoSelector:HTMLInputElement){
    if(!photoSelector.files) return;
   this.selectedImageFile = photoSelector.files[0]
   if(!this.selectedImageFile) return;
   var fileReader= new FileReader();
   fileReader.readAsDataURL(this.selectedImageFile)
   fileReader.addEventListener(
    "loadend", ev =>{
      
     var readableString= fileReader.result?.toString(); 
     var postPreviewImage = <HTMLImageElement>document.getElementById("post-preview-image")
     if(!readableString) return;
     postPreviewImage.src = readableString;
    }
   );
  }
}
