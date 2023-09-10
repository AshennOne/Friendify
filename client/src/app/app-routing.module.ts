import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Pages/home/home.component';
import { RegisterComponent } from './Pages/register/register.component';
import { ForgetPasswordComponent } from './Pages/forget-password/forget-password.component';
import { ConfirmEmailComponent } from './Pages/confirm-email/confirm-email.component';
import { MainComponent } from './Pages/main/main.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'confirmemail', component: ConfirmEmailComponent },
  { path: 'forgetpassword', component: ForgetPasswordComponent },
  { path: 'main', component: MainComponent },
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
