import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Follow } from '../_models/Follow';

@Injectable({
  providedIn: 'root'
})
export class FollowService {
  apiUrl = environment.apiUrl
  constructor(private http:HttpClient) {  
   }
  follow(userId:string){
    return this.http.post<Follow>(this.apiUrl+'follow/'+userId,{});
  }
  
  unfollow(userId:string){
    return this.http.delete(this.apiUrl+'follow/'+userId,{});
  }
  getFollowers(userId:string){
    return this.http.get<Follow[]>(this.apiUrl+"followers/"+userId);
  }
  getFollowed(userId:string){
    return this.http.get<Follow[]>(this.apiUrl+"followed/"+userId);
  }

}
