import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Comment } from '../_models/Comment';

@Injectable({
  providedIn: 'root',
})
export class CommentService {
  apiUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}
  GetCommentsForPost(postId:number){
    return this.http.get<Comment[]>(this.apiUrl+"comments/"+postId);
   }
   AddComment(comment:Comment){
    return this.http.post(this.apiUrl+"comments",comment);
   }
   EditComment(commentId:number,comment:Comment){
    return this.http.put(this.apiUrl+"comments/"+commentId,comment);
   }
   DeleteComment(commentId:number){
    return this.http.delete(this.apiUrl+"comments/"+commentId);
   }
}
