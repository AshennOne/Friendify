import { User } from "./User";

export interface Message{
  id?:number,
  senderId?:string,
  sender?:User,
  receiver?:User,
  receiverId?:string,
  content?:string,
  readDate:Date,
  sendDate:Date,
  read:boolean
}