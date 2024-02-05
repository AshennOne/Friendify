import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/User';
import { Post } from '../_models/Post';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  apiUrl = environment.apiUrl
  constructor(private http:HttpClient) { }
  getUserByEmail(email:string){
    return this.http.get<User>(this.apiUrl+'user/'+email)
  }
  getUserById(id:string){
    return this.http.get<User>(this.apiUrl+'user/id/'+id);
  }
  editUser(user:User){
    return this.http.put<User>(this.apiUrl+"user",user)
  }
  getUsers(){
    return this.http.get<User[]>(this.apiUrl+"user/all")
  }
  searchForUsers(searchstring:string){
    return this.http.get<User[]>(this.apiUrl+"user/search/"+ searchstring);
  }
}
