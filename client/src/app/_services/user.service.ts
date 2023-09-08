import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../_models/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  apiUrl = environment.apiUrl
  constructor(private http:HttpClient) { }
  getUserByEmail(email:string){
    return this.http.get<User>(this.apiUrl+'user/'+email)
  }
}
