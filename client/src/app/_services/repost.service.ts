import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Post } from '../_models/Post';

@Injectable({
  providedIn: 'root'
})
export class RepostService {
  apiUrl = environment.apiUrl
  constructor(private http:HttpClient) { }

  getAllReposts()
  {
    return this.http.get<Post[]>(this.apiUrl+"repost/all");
  }
  repost(originalPostId:number){
    return this.http.post(this.apiUrl+"repost/"+originalPostId,{},
    { responseType: 'text' });
  }
  unRepost(originalPostId:number){
    return this.http.delete(this.apiUrl+"repost/"+originalPostId,
    { responseType: 'text' });
  }
}
