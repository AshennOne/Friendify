import { Post } from "./Post";
import { User } from "./User";

export interface Like{
  id?:number,
  likedBy?:User,
  post?:Post,
  postId?:number,
}