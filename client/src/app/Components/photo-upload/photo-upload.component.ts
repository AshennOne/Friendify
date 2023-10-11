import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-photo-upload',
  templateUrl: './photo-upload.component.html',
  styleUrls: ['./photo-upload.component.css']
})
export class PhotoUploadComponent {

  @Input() selectedImageFile?: File;
  @Output() imageSelected = new EventEmitter<File>();
  previewImageUrl?: string;

  
  onPhotoSelected(photoSelector: HTMLInputElement) {
    if (!photoSelector.files) return;
    this.selectedImageFile = photoSelector.files[0];
    if (!this.selectedImageFile) return;
    var fileReader = new FileReader();
    fileReader.readAsDataURL(this.selectedImageFile);
    fileReader.addEventListener('loadend', (ev) => {
      var readableString = fileReader.result?.toString();
      var postPreviewImage = <HTMLImageElement>(
        document.getElementById('post-preview-image')
      );
      if (!readableString) return;
      postPreviewImage.src = readableString;
       this.imageSelected.emit(this.selectedImageFile);
    });
  }

  openGallery() {
    const input = document.getElementById('photo-upload');
    if (input) {
      input.click();
    }
  }

}
