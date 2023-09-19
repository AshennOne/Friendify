import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserAuth } from '../_models/UserAuth';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/User';
import { map, of, switchMap } from 'rxjs';
import { LikeService } from './like.service';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient,private likeService:LikeService) {}
  register(user: UserAuth) {
    return this.http.post(this.apiUrl + 'auth/register', user, {
      responseType: 'text',
    });
  }
  login(user: UserAuth) {
    return this.http.post<User>(this.apiUrl + 'auth/login', user);
  }
  forgetPassword(userId: string, code: string, newPassword: string) {
    return this.http.post(
      this.apiUrl +
        'auth/forgetpassword?userId=' +
        userId +
        '&code=' +
        code +
        '&newPassword=' +
        newPassword,
      {}
    );
  }
  sendConfirmEmail(email: string) {
    return this.http.post(
      this.apiUrl + 'auth/sendEmail?email=' + email + '&isPassword=false',
      {},
      { responseType: 'text' }
    );
  }
  sendForgetPassword(email: string, password: string) {
    return this.http.post(
      this.apiUrl +
        'auth/sendEmail?email=' +
        email +
        '&isPassword=true&password=' +
        password,
      {},
      { responseType: 'text' }
    );
  }
  confirmPassword(email: string) {
    return this.http.post(
      this.apiUrl + 'auth/sendEmail?email=' + email + '&isPassword=true',
      {}
    );
  }
  getCurrentUser() {
    var user = localStorage.getItem('user');
    
    if (!user) {
      return this.http.get<User>(this.apiUrl + 'auth/currentUser').pipe(
        switchMap((user) => {
          return this.likeService.GetLikedPostsForUser().pipe(
            map((posts) => {
              user.liked = posts;
              localStorage.setItem('user',JSON.stringify(user));
              return user; 
            })
          );
        })
      );
    }
    else {
      var parsedUser = JSON.parse(user) as User;
      return of(parsedUser)
    }
  }
}
