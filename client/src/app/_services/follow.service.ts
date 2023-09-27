import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FollowService {
  apiUrl = environment.apiUrl
  constructor(private http:HttpClient) {
    
   }
  constructor() { }
  
}
