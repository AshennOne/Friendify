import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ImageCroppedEvent, LoadedImage } from 'ngx-image-cropper';

@Component({
  selector: 'app-photo-upload',
  templateUrl: './photo-upload.component.html',
  styleUrls: ['./photo-upload.component.css'],
})
export class PhotoUploadComponent {
  @Input() isForUser = false;
  
  @Input() selectedImageFile?: File;
  @Output() imageSelected = new EventEmitter<File>();
  previewImageUrl?: string;
  imageChangedEvent: any = '';
  croppedImagePreview: any = '';
  filename = ''
isHidden = false
  onPhotoSelected(event: any) {
    this.imageChangedEvent = event;
    
  }
  constructor(private sanitizer: DomSanitizer) {}
  
  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
}
imageCropped(event: ImageCroppedEvent) {
  if(event.objectUrl && event.blob){
    this.croppedImagePreview = this.sanitizer.bypassSecurityTrustUrl(event.objectUrl);
    const file = new File([event.blob], this.filename, { type: 'image/jpeg' });
    
    this.selectedImageFile = file
   
  }
  

}
emitImage(){
  
  this.imageSelected.emit(this.selectedImageFile)
  this.isHidden = true
}
imageLoaded(image: LoadedImage) {
  const timestamp = new Date().getTime().toString();
  const randomString = Math.random().toString(36).substring(2, 8);
  this.filename = timestamp + "/"+randomString
}
cropperReady() {
   
}
loadImageFailed() {
   
}
}
