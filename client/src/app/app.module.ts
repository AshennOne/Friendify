import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Pages/home/home.component';
import { FormInputComponent } from './Components/form-input/form-input.component';
import { HttpClientModule } from '@angular/common/http';
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
    
  ],
  imports: [
    BsDatepickerModule.forRoot(),
    BrowserAnimationsModule,
    BrowserModule,
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
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
