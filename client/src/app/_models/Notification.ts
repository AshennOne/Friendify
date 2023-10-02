export interface Notification {
  id: number;
  fromUserId: string;
  toUserId: string;
  isRead: boolean;
  createDate: Date;
  fromUserName: string;
  message: string;
  type: number;
  elementId: number;
  imgUrl: string;
}