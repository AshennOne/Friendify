import { Comment } from "./Comment";
import { User } from "./User";

export interface Post{
  textContent?:string,
  author?:User,
  authorId?:User,
  created?:Date
  imgUrl?:string,
  id?:number,
  likesCount?:number,
  commentsCount?:number,
  repostCount?:number,
  comments?:Comment[]
  repostedFromId?:number
}