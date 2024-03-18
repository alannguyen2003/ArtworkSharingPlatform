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

  constructor(private artworkService: ArtworkService) {
    this.getAnimeArtworks()
  }
  ngOnInit(): void {

  }
  async getAnimeArtworks() {
    this.artworkService.getArtworksByGenreId(1, this.pageNumber, this.pageSize)
      .subscribe({
        next: animeArtworks => {
          this.animeArtworks = animeArtworks;
        }
      }
      );
  }
}
