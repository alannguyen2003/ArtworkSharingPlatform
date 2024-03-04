import {Component, Input} from '@angular/core';
import {Artwork} from "../../../_model/artwork.model";

@Component({
  selector: 'app-artwork-card',
  templateUrl: './artwork-card.component.html',
  styleUrls: ['./artwork-card.component.css']
})
export class ArtworkCardComponent {
  @Input() artwork: Artwork | undefined;
}
