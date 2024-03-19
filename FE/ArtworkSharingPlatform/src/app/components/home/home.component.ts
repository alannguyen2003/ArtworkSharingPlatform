import { Component, OnInit } from '@angular/core';
import { ArtworkService } from 'src/app/_services/artwork.service';
import { Artwork } from '../../_model/artwork.model';
import { ArtworkCarouselComponent } from './artwork-carousel/artwork-carousel.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  pageNumber: number = 1
  pageSize: number = 15
  animeArtworks: Artwork[] = [];
  landscapeArtworks: Artwork[] = [];
  fictionArtworks: Artwork[] = [];
  portraitArtworks: Artwork[] = [];

  constructor(private artworkService: ArtworkService) {
    // this.getAnimeArtworks()
    // this.getLandscapeArtworks()
    // this.getFictionArtworks()
    // this.getPortraitArtworks()
    this.getArtworkData()
  }
  ngOnInit(): void {

  }

  async getArtworkData() {
    this.artworkService.getArtworksByGenreId(1, this.pageNumber, this.pageSize)
      .subscribe({
        next: landscapeArtworks => {
          this.landscapeArtworks = landscapeArtworks;
        }
      }
      );

    this.artworkService.getArtworksByGenreId(2, this.pageNumber, this.pageSize)
      .subscribe({
        next: portraitArtworks => {
          this.portraitArtworks = portraitArtworks;
        }
      }
      );

    this.artworkService.getArtworksByGenreId(3, this.pageNumber, this.pageSize)
      .subscribe({
        next: animeArtworks => {
          this.animeArtworks = animeArtworks;
        }
      }
      );


    this.artworkService.getArtworksByGenreId(4, this.pageNumber, this.pageSize)
      .subscribe({
        next: fictionArtworks => {
          this.fictionArtworks = fictionArtworks;
        }
      }
      );
  }
}
