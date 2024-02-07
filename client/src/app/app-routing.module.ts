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
import { AuthGuard } from './_guards/auth.guard';
import { AntiAuthGuard } from './_guards/anti-auth.guard';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AntiAuthGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [AntiAuthGuard] },
  { path: 'confirmemail', component: ConfirmEmailComponent, canActivate: [AntiAuthGuard] },
  { path: 'forgetpassword', component: ForgetPasswordComponent, canActivate: [AntiAuthGuard] },
  { path: 'user/:id', component: DetailsComponent, canActivate: [AuthGuard] },
  { path: 'main', component: MainComponent, canActivate: [AuthGuard] },
  { path: 'messages', component: MessagesComponent, canActivate: [AuthGuard] },
  { path: 'messages/:id', component: MessageThreadComponent, canActivate: [AuthGuard] },
  { path: 'notifications', component: NotificationsComponent, canActivate: [AuthGuard] },
  { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
  { path: '**', component: HomeComponent, canActivate: [AntiAuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
