import {Component, OnInit} from '@angular/core';
import {Artwork} from "../../../_model/artwork.model";
import {ArtworkImage} from "../../../_model/artworkImage.model";
import {ArtworkService} from "../../../_services/artwork.service";
import {User} from "../../../_model/user.model";
import {AccountService} from "../../../_services/account.service";
import {take} from "rxjs";
import {Genre} from "../../../_model/genre.model";

@Component({
  selector: 'app-artwork-post',
  templateUrl: './artwork-post.component.html',
  styleUrls: ['./artwork-post.component.css']
})
export class ArtworkPostComponent implements OnInit{
  artworkImages: ArtworkImage[] = [];
  artwork: Artwork = {} as Artwork;
  user: User | undefined;
  genres: Genre[] = [];

  constructor(private artworkService: ArtworkService, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user;
      }
    });
  }
  ngOnInit() {
    this.loadGenres();
  }

  onImageAdded(imageData: ArtworkImage) {
    if (this.artworkImages.length == 0) imageData.isThumbnail = true;
    this.artworkImages.push(imageData);
  }

  setThumbnailImage(image: ArtworkImage) {
    this.artworkImages.forEach(x => x.isThumbnail = false);
    image.isThumbnail = true;
  }

  postArtwork() {
    if (!this.artwork || !this.user) return;
    this.artwork.artworkImages = this.artworkImages;
    this.artworkService.addArtwork(this.artwork).subscribe({
      next: artwork => {
        console.log(artwork);
      }
    });
  }
  loadGenres() {
    this.artworkService.getGenreForArtwork().subscribe({
      next: genres => {
        this.genres = genres;
      }
    });
  }

}
