import { Post } from "./Post";
import { User } from "./User";

export interface Comment{
  id?:number,
  commentedBy?:User,
  post?:Post,
  postId?:number,
  content?:string,
  created?:Date
}