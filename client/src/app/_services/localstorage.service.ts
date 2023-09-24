import { Injectable } from '@angular/core';
import { User } from '../_models/User';
import { Post } from '../_models/Post';

@Injectable({
  providedIn: 'root'
})
export class LocalstorageService {

  constructor() { }
  addPostToLiked(post:Post){
    var user = localStorage.getItem('user');
    if(!user) return;
    var parsedUser = JSON.parse(user) as User;
    var posts = parsedUser.liked
    if(!posts) return;
    posts.push(post);
    var stringifiedUser = JSON.stringify(parsedUser);
    localStorage.setItem('user',stringifiedUser)
  }  
  removePostFromLiked(post:Post){
    var user = localStorage.getItem('user');
    if(!user) return;
    var parsedUser = JSON.parse(user) as User;
    var posts = parsedUser.liked
    if(!posts) return;
  parsedUser.liked= posts.filter(p => p.id != post.id);
    var stringifiedUser = JSON.stringify(parsedUser);
    localStorage.setItem('user',stringifiedUser)
  }
  addPostToReposted(post:Post){
    var user = localStorage.getItem('user');
    if(!user) return;
    var parsedUser = JSON.parse(user) as User;
    var posts = parsedUser.reposted
    if(!posts) return;
    posts.push(post);
    var stringifiedUser = JSON.stringify(parsedUser);
    localStorage.setItem('user',stringifiedUser)
  }  
  removePostFromReposted(post:Post){
    var user = localStorage.getItem('user');
    if(!user) return;
    var parsedUser = JSON.parse(user) as User;
    var posts = parsedUser.reposted
    if(!posts) return;
  parsedUser.reposted= posts.filter(p => p.id != post.id);
    var stringifiedUser = JSON.stringify(parsedUser);
    localStorage.setItem('user',stringifiedUser)
  }
}
