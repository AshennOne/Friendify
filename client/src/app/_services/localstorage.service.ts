import { Injectable } from '@angular/core';
import { User } from '../_models/User';
import { Post } from '../_models/Post';
import { Follow } from '../_models/Follow';

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
  addFollow(follow:Follow){
    var user = localStorage.getItem('user');
    if(!user) return;
    var parsedUser = JSON.parse(user) as User;
    var followed = parsedUser.followed
    if(!followed) return;
    followed.push(follow)
    var stringifiedUser = JSON.stringify(parsedUser);
    localStorage.setItem('user',stringifiedUser)
  }
  removeFollow(followedId:string){
    var user = localStorage.getItem('user');
    if(!user) return;
    var parsedUser = JSON.parse(user) as User;
    var followed = parsedUser.followed
    if(!followed) return;
    parsedUser.followed = followed.filter(f => f.followedId != followedId)
    var stringifiedUser = JSON.stringify(parsedUser);
    localStorage.setItem('user',stringifiedUser)
  }
  changePhoto(imgUrl:string){
    var user = localStorage.getItem('user');
    if(!user) return;
    var parsedUser = JSON.parse(user) as User;
    if(parsedUser){
      parsedUser.imgUrl = imgUrl;
      var stringifiedUser = JSON.stringify(parsedUser);
      localStorage.setItem('user',stringifiedUser)
    }
  }
}
