import {Artwork} from "./artwork.model";
import {Follow} from "./follow.model";

export interface UserProfile {
  name: string;
  email: string;
  twitterLink: string;
  facebookLink: string;
  phoneNumber: string;
  description: string;
  imageUrl: string;
  artworks: Artwork[];
  isFollowedByUsers: Follow[];
}
