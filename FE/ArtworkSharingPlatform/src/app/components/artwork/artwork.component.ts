import { Component, OnInit } from '@angular/core';
import { Artwork } from "../../_model/artwork.model";
import { ArtworkService } from "../../_services/artwork.service";
import { UserParams } from "../../_model/userParams.model";
import { Pagination } from "../../_model/pagination.model";
import { Genre } from "../../_model/genre.model";

@Component({
  selector: 'app-artwork',
  templateUrl: './artwork.component.html',
  styleUrls: ['./artwork.component.css']
})
export class ArtworkComponent implements OnInit {
  artworks: Artwork[] = [];
  userParams: UserParams | undefined;
  pagination: Pagination | undefined;
  orderPriceList = [{ value: 'lowPrice', display: 'Low to High' },
  { value: 'highPrice', display: 'High to Low' }];
  genres: Genre[] = [];
  selectedGenres: any[] = [];
  constructor(private artworkService: ArtworkService) {
  }
  ngOnInit() {
    this.userParams = this.artworkService.getUserParams();
    this.artworkService.loadUserParams().subscribe({
      next: config => {
        if (this.userParams) {
          this.userParams.pageSize = config.totalItemPerPage;
          this.userParams.rowSize = config.rowSize;
        }
        this.loadArtworks();
      }
    });
    this.loadGenres();
  }

  loadGenres() {
    this.artworkService.getGenreForArtwork().subscribe({
      next: genres => {
        this.genres = genres;
      }
    });
  }

  loadArtworks() {
    if (this.userParams) {
      this.userParams.genreIds = this.selectedGenres;
      this.artworkService.setUserParams(this.userParams);
      this.artworkService.getArtworks(this.userParams).subscribe({
        next: response => {
          console.log(response)
          if (response?.result && response.pagination) {
            this.artworks = response.result;
            this.pagination = response.pagination;
          }
        }
      });
    }
  }

  resetFilters() {
    this.userParams = this.artworkService.resetUserParams();
    this.loadArtworks();
  }

  async pageChanged(event: any) {
    if (this.userParams && this.userParams?.pageNumber != event.page) {
      this.userParams.pageNumber = event.page;
      this.artworkService.setUserParams(this.userParams);
      this.loadArtworks();
    }
  }

  onGenreSelected(genreId: number) {
    const index = this.selectedGenres.indexOf(genreId);
    index !== -1 ? this.selectedGenres.splice(index, 1) : this.selectedGenres.push(genreId);
    console.log(this.selectedGenres);
    this.loadArtworks()
  }

  isSelectedGenre(genreId: number) {
    return this.selectedGenres.some(x => x == genreId);
  }


}
