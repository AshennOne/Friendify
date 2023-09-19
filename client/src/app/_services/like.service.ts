import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Post } from '../_models/Post';

@Injectable({
  providedIn: 'root'
})
export class LikeService {
  apiUrl = environment.apiUrl
  constructor(private http:HttpClient) {
    
   }
   GetLikedPostsForUser(){
    return this.http.get<Post[]>(this.apiUrl+"postlikes");
   }
   AddLike(postId:number){
    return this.http.post(this.apiUrl+"postlikes/"+postId,{});
   }
   RemoveLike(postId:number){
    return this.http.delete(this.apiUrl+"postlikes/"+postId);
   }
}
