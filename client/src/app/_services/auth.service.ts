import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserAuth } from '../_models/UserAuth';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/User';
import { catchError, forkJoin, map, of, switchMap } from 'rxjs';
import { LikeService } from './like.service';
import { RepostService } from './repost.service';
@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient,private likeService:LikeService,private repostService:RepostService) {}
  register(user: UserAuth) {
    localStorage.clear()
    return this.http.post(this.apiUrl + 'auth/register', user, {
      responseType: 'text',
    });
  }
  login(user: UserAuth) {
    localStorage.clear()
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
          const likedPosts$ = this.likeService.GetLikedPostsForUser();
          const repostedPosts$ = this.repostService.getAllReposts();
    
          return forkJoin({ likedPosts$, repostedPosts$ }).pipe(
            map((result) => {
              user.liked = result.likedPosts$;
              user.reposted = result.repostedPosts$;
              localStorage.setItem('user', JSON.stringify(user));
              return user;
            }),
            catchError((error) => {
             
              console.error('Error:', error);
              return of(user); 
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
