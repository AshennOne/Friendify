import { Post } from "./Post"

export interface User{
  firstName?:string,
  lastName?:string,
  userName?:string,
  email?:string,
  imgUrl?:string,
  id?:string,
  emailConfirmed?:boolean,
  gender?:string,
  dateOfBirth?:Date,
  token?:string
  liked?:Post[]
}