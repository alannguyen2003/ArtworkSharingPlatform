import {Artwork} from "./artwork.model";

export interface UserProfile {
  name: string;
  twitterLink: string;
  facebookLink: string;
  phoneNumber: string;
  description: string;
  imageUrl: string;
  artworks: Artwork[];
}
