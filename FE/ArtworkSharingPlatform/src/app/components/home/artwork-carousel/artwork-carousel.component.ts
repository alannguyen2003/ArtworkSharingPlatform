import { Component, OnInit, Input } from '@angular/core';
import { ArtworkService } from 'src/app/_services/artwork.service';
import { Artwork } from 'src/app/_model/artwork.model';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-artwork-carousel',
    templateUrl: './artwork-carousel.component.html',
    styleUrls: ['./artwork-carousel.component.css']
})
export class ArtworkCarouselComponent implements OnInit {
    @Input() artworks: Artwork[] = []
    constructor(private artworkService: ArtworkService) { }
    ngOnInit(): void {

    }
}