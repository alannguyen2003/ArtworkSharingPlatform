import {User} from "./user.model";

export interface Artwork {
  id: number;
  title: string;
  createdDate: Date;
  description: string;
  imageUrl: string;
  price: number;
  releaseCount: number;
  user: User;
}
