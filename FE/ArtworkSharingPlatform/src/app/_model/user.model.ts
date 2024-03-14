import {Follow} from "./follow.model";

export interface User {
  name: string;
  token: string;
  phoneNumber: string;
  email: string;
  description: string;
  imageUrl: string;
  facebookLink: string;
  twitterLink: string;
  roles: string[];
}
