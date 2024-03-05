import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {Artwork} from "../../../_model/artwork.model";
import {ArtworkImage} from "../../../_model/artworkImage.model";
import {CommonModule} from "@angular/common";
import {TabsModule} from "ngx-bootstrap/tabs";
import {GalleryItem, GalleryModule, ImageItem} from "ng-gallery";

@Component({
  selector: 'app-artwork-detail',
  standalone: true,
  templateUrl: './artwork-detail.component.html',
  styleUrls: ['./artwork-detail.component.css'],
  imports: [CommonModule, TabsModule, GalleryModule]
})
export class ArtworkDetailComponent implements OnInit{

  artwork: Artwork | undefined;
  images: GalleryItem[] = [];

  constructor(private route: ActivatedRoute) {
  }
  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.artwork = data['artwork'];
      }
    });
    this.getImages();
  }

  getImages() {
    if (!this.artwork) return;

    for (const image of this.artwork.artworkImages) {
      this.images.push(new ImageItem({src: image.imageUrl, thumb: image.imageUrl}));
    }
  }
}
