import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  TemplateRef,
} from '@angular/core';
import {
  Storage,
  ref,
  uploadBytesResumable,
  getDownloadURL,
} from '@angular/fire/storage';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/_models/User';
import { ConnectorService } from 'src/app/_services/connector.service';
import { LocalstorageService } from 'src/app/_services/localstorage.service';
import { PresenceService } from 'src/app/_services/presence.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-edit-photo',
  templateUrl: './edit-photo.component.html',
  styleUrls: ['./edit-photo.component.css'],
})
export class EditPhotoComponent implements OnInit {
  @Input() imgUrl?: string;
  @Output() reload = new EventEmitter<boolean>();
  modalRef?: BsModalRef;
  selectedImageFile?: File;
  constructor(
    public modalService: BsModalService,
    private storage: Storage,
    private userService: UserService,
    private toastr: ToastrService,
    private connectorService: ConnectorService,
    private localStorageService: LocalstorageService,
   
  ) {}

  ngOnInit(): void {}
  changeImage(event: any) {
    this.selectedImageFile = event;
  }
  hideModal() {
    this.modalService.hide();
    this.selectedImageFile = undefined;
  }
  editPhoto() {
    var user = { imgUrl: this.imgUrl } as User;
    this.userService.editUser(user).subscribe({
      next: () => {
        this.toastr.success('Succesfuly updated profile picture');
        this.connectorService.imgUrl.emit(user.imgUrl);
        this.reload.emit(true);
        this.localStorageService.changePhoto(user.imgUrl + '');
        this.modalService.hide();
      },
    });
  }
  openModal(template: TemplateRef<any>) {
    this.modalService.show(template);
  }
  onSubmit() {
    if (!this.selectedImageFile) return;
    else {
      var storageRef = ref(
        this.storage,
        'folder/' + this.selectedImageFile.name
      );
      var uploadTask = uploadBytesResumable(storageRef, this.selectedImageFile);
      uploadTask.on(
        'state_changed',
        () => {},
        (error) => {
          console.log(error);
        },
        () => {
          getDownloadURL(uploadTask.snapshot.ref).then((downloadUrl) => {
            this.imgUrl = downloadUrl;
            this.editPhoto();
          });
        }
      );
    }
  }
}
