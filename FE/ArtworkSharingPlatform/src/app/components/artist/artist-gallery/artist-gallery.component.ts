import {Component, OnInit} from '@angular/core';
import {Artwork} from "../../../_model/artwork.model";
import {ArtworkService} from "../../../_services/artwork.service";

@Component({
  selector: 'app-artist-gallery',
  templateUrl: './artist-gallery.component.html',
  styleUrls: ['./artist-gallery.component.css']
})
export class ArtistGalleryComponent implements OnInit{
  artworks: Artwork[] = [];

  constructor(private artworkService: ArtworkService) {
  }
  ngOnInit() {
    this.artworkService.getArtistArtwork().subscribe({
      next: artworks => {
        if (artworks) this.artworks = artworks;
      }
    });
  }
}
