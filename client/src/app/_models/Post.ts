import { User } from "./User";

export interface Post{
  textContent?:string,
  author?:User,
  authorId?:User,
  created?:Date
  imgUrl?:string,
  id:number
}