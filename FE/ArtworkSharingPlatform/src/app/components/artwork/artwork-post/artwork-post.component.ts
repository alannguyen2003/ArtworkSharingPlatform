import {Component, OnInit} from '@angular/core';
import {Artwork} from "../../../_model/artwork.model";
import {ArtworkImage} from "../../../_model/artworkImage.model";
import {ArtworkService} from "../../../_services/artwork.service";
import {User} from "../../../_model/user.model";
import {AccountService} from "../../../_services/account.service";
import {take} from "rxjs";
import {Genre} from "../../../_model/genre.model";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";

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
  validationErrors : string[] = [];

  constructor(private artworkService: ArtworkService,
              private accountService: AccountService,
              private router: Router,
              private toastr: ToastrService) {
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
    this.validationErrors = [];
    this.artwork.artworkImages = this.artworkImages;
    if (this.artworkImages.length <= 0) {
      this.validationErrors.push("You need to add at least one image to post an artwork");
      return;
    }
    this.artworkService.addArtwork(this.artwork).subscribe({
      next: artwork => {
        this.toastr.success("Post artwork successfullly!");
        this.router.navigateByUrl('/artist/gallery');
      },
      error: errs => this.validationErrors = errs
    });
  }
  loadGenres() {
    this.artworkService.getGenreForArtwork().subscribe({
      next: genres => {
        this.genres = genres;
      }
    });
  }
  deleteImage(image: ArtworkImage) {
    this.artworkService.deleteImageDuringPostArtwork(image).subscribe({
      next: _ => {
        this.toastr.success('Image delete success');
        this.artworkImages = this.artworkImages.filter(x => x.publicId != image.publicId);
      }
    })
  }

}
