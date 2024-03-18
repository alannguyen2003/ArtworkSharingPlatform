import { Component, OnInit, Input, ViewChild } from '@angular/core';
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
    images = [62, 83, 466, 965, 982, 1043, 738].map((n) => `https://picsum.photos/id/${n}/900/500`);
    paused = false;
    unpauseOnArrow = false;
    pauseOnIndicator = false;
    pauseOnHover = true;
    pauseOnFocus = true;

    @Input() artworks: Artwork[] = []
    @ViewChild('carousel', { static: true }) carousel: NgbCarousel;

    ngOnInit(): void {

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