import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription, interval } from 'rxjs';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css'],
})
export class ConfirmEmailComponent implements OnInit {
  isCooldown = false;
  countdown = 60;
  mail = ''
  private countdownSubscription?: Subscription;
  constructor(private authService:AuthService,private userService:UserService,private toastr:ToastrService,private router:Router) {
    
  }

  ngOnInit(): void {
    this.mail = localStorage.getItem('email') + ''
  }
  startCooldown(): void {
    if (!this.isCooldown) {
      this.authService.sendConfirmEmail(this.mail).subscribe({
        next:(res)=>{
        }
      })
      this.isCooldown = true;
      this.countdownSubscription = interval(1000).subscribe(() => {
        if (this.countdown > 0) {
          this.countdown--;
        } else {
          this.stopCooldown();
        }
      });
    }}
    stopCooldown(): void {
   
      this.isCooldown = false;
      this.countdown = 60; 
      if (this.countdownSubscription) {
        this.countdownSubscription.unsubscribe();
      }
    }
    verify(){
      this.userService.getUserByEmail(this.mail).subscribe({
        next:(user)=>{
          if(user.emailConfirmed){
            this.toastr.success("Succesfully confirmed")
            localStorage.setItem("token",user.token+'')
            localStorage.setItem('username',user.userName+'')
            this.router.navigateByUrl("main");
            localStorage.removeItem("email")
            
          }else{
            this.toastr.error("Email not confirmed")
          }
        }
      })
    }
}
