import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Pages/home/home.component';
import { FormInputComponent } from './Components/form-input/form-input.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './Pages/register/register.component';
import { ForgetPasswordComponent } from './Pages/forget-password/forget-password.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ConfirmEmailComponent } from './Pages/confirm-email/confirm-email.component';
import { MainComponent } from './Pages/main/main.component';
import { ToastrModule } from 'ngx-toastr';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PostComponent } from './Components/post/post.component';
import { TokenInterceptor } from './_Interceptors/token.interceptor';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NewPostComponent } from './Components/new-post/new-post.component';
import {MatDialogModule} from '@angular/material/dialog';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {provideAuth,getAuth} from '@angular/fire/auth'
import {provideFirestore,getFirestore} from '@angular/fire/firestore'
import{provideStorage,getStorage} from '@angular/fire/storage';
import {initializeApp,provideFirebaseApp} from '@angular/fire/app'
import { environment } from 'src/environments/environment.prod';
import { LikeComponent } from './Components/post/like/like.component';
import { CommentComponent } from './Components/post/comment/comment.component';
import { RetweetComponent } from './Components/post/retweet/retweet.component';
import { TimeagoModule } from "ngx-timeago";
import { DetailsComponent } from './Pages/details/details.component';
import {MatDividerModule} from '@angular/material/divider';
@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FormInputComponent,
    RegisterComponent,
    ForgetPasswordComponent,
    ConfirmEmailComponent,
    MainComponent,
    NavbarComponent,
    PostComponent,
    NewPostComponent,
    LikeComponent,
    CommentComponent,
    RetweetComponent,
    DetailsComponent,
   
  ],
  imports: [
    BsDatepickerModule.forRoot(),
    BrowserAnimationsModule,
    BrowserModule,
    provideFirebaseApp(()=>initializeApp(environment.firebaseConfig)),
    provideAuth(()=> getAuth()),
    provideFirestore(()=>getFirestore()),
    provideStorage(()=>getStorage()),
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BsDropdownModule.forRoot(),
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 4000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    ModalModule.forRoot(),
    MatIconModule,
    MatDialogModule,
    MatButtonModule,
    MatCardModule,
    TimeagoModule.forRoot(),
    MatDividerModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
