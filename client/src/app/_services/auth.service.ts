import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserAuth } from '../_models/UserAuth';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  apiUrl = environment.apiUrl
  constructor(private http:HttpClient) {
    
   }
   register(user:UserAuth){
    return this.http.post(this.apiUrl+'auth/register',user)
   }
   login(user:UserAuth){
    return this.http.post(this.apiUrl+'auth/login',user);
   }
   forgetPassword(userId:string,code:string,newPassword:string){
    return this.http.post(this.apiUrl+'auth/forgetpassword?userId='+userId+'&code='+code+'&newPassword='+newPassword,{})
   }
   confirmEmail(email:string){
    return this.http.post(this.apiUrl+'auth/sendEmail?email='+email+'&isPassword=false',{})
   }
   confirmPassword(email:string){
    return this.http.post(this.apiUrl+'auth/sendEmail?email='+email+'&isPassword=true',{})
   }
}
