import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Post } from '../_models/Post';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}
  getAllPosts() {
    return this.http.get<Post[]>(this.baseUrl + 'posts/all');
  }
  searchPosts(searchstring:string){
    return this.http.get<Post[]>(this.baseUrl+"posts/search?searchstring="+searchstring);
  }
  getPostsForCurrentUser() {
    return this.http.get<Post[]>(this.baseUrl + 'posts');
  }
  addPost(post:Post){
    return this.http.post<Post>(this.baseUrl+'posts',post);
  }
  deletePost(id:number){
    return this.http.delete(this.baseUrl+'posts/'+id);
  }
  getPostsForUserId(id:string){
    return this.http.get<Post[]>(this.baseUrl+"posts/user/"+id);
  }
}
