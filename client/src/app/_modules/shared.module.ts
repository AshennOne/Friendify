import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDividerModule } from '@angular/material/divider';
import { TimeagoModule } from 'ngx-timeago';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ImageCropperModule } from 'ngx-image-cropper';
import { ToastrModule } from 'ngx-toastr';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { environment } from 'src/environments/environment.prod';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { MatBadgeModule } from '@angular/material/badge';
import { NgxLoadingModule } from 'ngx-loading';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot({
      timeOut: 4000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    ModalModule.forRoot(),
    MatIconModule,
    MatDialogModule,
    MatButtonModule,
    BsDatepickerModule.forRoot(),
    MatCardModule,
    BsDropdownModule.forRoot(),
    TimeagoModule.forRoot(),
    MatDividerModule,
    MatBadgeModule,
    NgxLoadingModule.forRoot({}),
    ImageCropperModule,
  ],
  exports: [
    ToastrModule,
    ModalModule,
    MatIconModule,
    MatDialogModule,
    MatButtonModule,
    BsDatepickerModule,
    MatCardModule,
    BsDropdownModule,
    TimeagoModule,
    MatDividerModule,
    MatBadgeModule,
    NgxLoadingModule,
    ImageCropperModule,
  ],
})
export class SharedModule {}
