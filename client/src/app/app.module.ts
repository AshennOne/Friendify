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
import { ConfirmEmailComponent } from './Pages/confirm-email/confirm-email.component';
import { MainComponent } from './Pages/main/main.component';
import { NavbarComponent } from './Components/navbar/navbar.component';
import { PostComponent } from './Components/post/post.component';
import { TokenInterceptor } from './_Interceptors/token.interceptor';
import { NewPostComponent } from './Components/new-post/new-post.component';
import { LikeComponent } from './Components/post/like/like.component';
import { CommentComponent } from './Components/post/comment/comment.component';
import { RetweetComponent } from './Components/post/retweet/retweet.component';
import { DetailsComponent } from './Pages/details/details.component';
import { NotificationsComponent } from './Pages/notifications/notifications.component';
import { MessagesComponent } from './Pages/messages/messages.component';
import { UsersComponent } from './Pages/users/users.component';
import { NotificationComponent } from './Components/notification/notification.component';
import { SharedModule } from './_modules/shared.module';
import { PostModalComponent } from './Components/post-modal/post-modal.component';
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
    NotificationsComponent,
    MessagesComponent,
    UsersComponent,
    NotificationComponent,
    PostModalComponent,
   
  ],
  imports: [
    
    BrowserAnimationsModule,
    BrowserModule,
    
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    SharedModule,
    ReactiveFormsModule
    
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
