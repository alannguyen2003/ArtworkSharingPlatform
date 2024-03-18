import { Component, OnInit, Input, ViewChild, SimpleChanges } from '@angular/core';
import { NgbCarousel, NgbCarouselModule, NgbSlideEvent, NgbSlideEventSource } from '@ng-bootstrap/ng-bootstrap';
import { ArtworkService } from 'src/app/_services/artwork.service';
import { Artwork } from 'src/app/_model/artwork.model';
import { CommonModule } from '@angular/common';



@Component({
    selector: 'app-artwork-carousel',
    templateUrl: './artwork-carousel.component.html',
    styleUrls: ['./artwork-carousel.component.css']
})
export class ArtworkCarouselComponent implements OnInit {
    paused = false;
    unpauseOnArrow = false;
    pauseOnIndicator = false;
    pauseOnHover = true;
    pauseOnFocus = true;
    chunkSize: number = 4;
    artworkChunks: Artwork[][] = [];
    @Input() artworks: Artwork[] = []

    @ViewChild('carousel', { static: true }) carousel: NgbCarousel;

    ngOnInit(): void {


    }
    ngOnChanges(changes: SimpleChanges) {
        if (changes['artworks']) {
            this.artworkChunks = this.divideArtworksIntoCarouselChunks(this.artworks, this.chunkSize)
        }
    }

    divideArtworksIntoCarouselChunks(arr: any[], chunkSize: number): Artwork[][] {
        const chunks = [];
        for (let i = 0; i < arr.length; i += chunkSize) {
            chunks.push(arr.slice(i, i + chunkSize));
            console.log("Current chunks: " + chunks);
        }
        return chunks;
    }
    togglePaused() {
        if (this.paused) {
            this.carousel.cycle();
        } else {
            this.carousel.pause();
        }
        this.paused = !this.paused;
    }

    onSlide(slideEvent: NgbSlideEvent) {
        console.log('Api will be called!')
        if (
            this.unpauseOnArrow &&
            slideEvent.paused &&
            (slideEvent.source === NgbSlideEventSource.ARROW_LEFT || slideEvent.source === NgbSlideEventSource.ARROW_RIGHT)
        ) {
            this.togglePaused();
        }
        if (this.pauseOnIndicator && !slideEvent.paused && slideEvent.source === NgbSlideEventSource.INDICATOR) {
            this.togglePaused();
        }
    }
}