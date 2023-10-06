import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDividerModule } from '@angular/material/divider';
import { TimeagoModule } from 'ngx-timeago';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { initializeApp, provideFirebaseApp } from '@angular/fire/app';
import { getAuth, provideAuth } from '@angular/fire/auth';
import { getStorage, provideStorage } from '@angular/fire/storage';
import { getFirestore, provideFirestore } from '@angular/fire/firestore';
import { environment } from 'src/environments/environment.prod';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import {MatBadgeModule} from '@angular/material/badge';

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
    provideFirebaseApp(() => initializeApp(environment.firebaseConfig)),
    BsDatepickerModule.forRoot(),
    provideAuth(() => getAuth()),
    provideFirestore(() => getFirestore()),
    provideStorage(() => getStorage()),
    MatCardModule,
    BsDropdownModule.forRoot(),
    TimeagoModule.forRoot(),
    MatDividerModule,
    MatBadgeModule
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
    MatBadgeModule
  ],
})
export class SharedModule {}
