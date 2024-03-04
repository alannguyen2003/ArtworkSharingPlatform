import {Component, OnInit} from '@angular/core';
import {Artwork} from "../../_model/artwork.model";
import {ArtworkService} from "../../_services/artwork.service";

@Component({
  selector: 'app-artwork',
  templateUrl: './artwork.component.html',
  styleUrls: ['./artwork.component.css']
})
export class ArtworkComponent implements OnInit{
  artworks: Artwork[] = [];

  constructor(private artworkService: ArtworkService) {
  }
  ngOnInit() {
    this.artworks = this.artworkService.getArtworks();
  }
}
