import {Component, OnInit} from '@angular/core';
import {Artwork} from "../../_model/artwork.model";
import {ArtworkService} from "../../_services/artwork.service";
import {UserParams} from "../../_model/userParams.model";
import {Pagination} from "../../_model/pagination.model";

@Component({
  selector: 'app-artwork',
  templateUrl: './artwork.component.html',
  styleUrls: ['./artwork.component.css']
})
export class ArtworkComponent implements OnInit{
  artworks: Artwork[] = [];
  userParams: UserParams | undefined;
  pagination: Pagination | undefined;
  orderPriceList = [{value: 'lowPrice', display: 'Low to High'},
    {value: 'highPrice', display: 'High to Low'}];
  constructor(private artworkService: ArtworkService) {
    this.userParams = this.artworkService.getUserParams();
  }
  ngOnInit() {
    this.loadArtworks();
    console.log(this.artworks);
  }

  loadArtworks() {
    if (this.userParams) {
      this.artworkService.setUserParams(this.userParams);
      this.artworkService.getArtworks(this.userParams).subscribe({
        next: response => {
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

  pageChanged(event : any) {
    if (this.userParams && this.userParams?.pageNumber != event.page) {
      this.userParams.pageNumber = event.page;
      this.artworkService.setUserParams(this.userParams);
      this.loadArtworks();
    }
  }
}
