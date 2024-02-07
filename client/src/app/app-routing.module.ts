import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Pages/home/home.component';
import { RegisterComponent } from './Pages/register/register.component';
import { ForgetPasswordComponent } from './Pages/forget-password/forget-password.component';
import { ConfirmEmailComponent } from './Pages/confirm-email/confirm-email.component';
import { MainComponent } from './Pages/main/main.component';
import { DetailsComponent } from './Pages/details/details.component';
import { MessagesComponent } from './Pages/messages/messages.component';
import { NotificationsComponent } from './Pages/notifications/notifications.component';
import { UsersComponent } from './Pages/users/users.component';
import { MessageThreadComponent } from './Pages/message-thread/message-thread.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'confirmemail', component: ConfirmEmailComponent },
  { path: 'forgetpassword', component: ForgetPasswordComponent },
  {path: 'user/:id', component:DetailsComponent},
  { path: 'main', component: MainComponent },
  { path: 'messages', component: MessagesComponent },
  { path: 'messages/:id', component: MessageThreadComponent },
  { path: 'notifications', component: NotificationsComponent },
  { path: 'users', component: UsersComponent },
  { path: '**', component: HomeComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
