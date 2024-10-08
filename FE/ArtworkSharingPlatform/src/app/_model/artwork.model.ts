import {User} from "./user.model";
import {ArtworkImage} from "./artworkImage.model";
import {ArtworkLike} from "./artworkLike.model";

export interface Artwork {
  id: number;
  title: string;
  createdDate: Date;
  description: string;
  imageUrl: string;
  genreId: number;
  genreName: string
  price: number;
  user: User;
  artworkImages: ArtworkImage[];
  likes: ArtworkLike[];
}
