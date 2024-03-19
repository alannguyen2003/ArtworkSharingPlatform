import {Component, HostListener, ViewChild} from '@angular/core';
import {RequestArtwork} from "../../../_model/request-artwork.model";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-request-artwork',
  templateUrl: './request-artwork.component.html',
  styleUrls: ['./request-artwork.component.css']
})
export class RequestArtworkComponent {
  requestArtwork: RequestArtwork | undefined;
  validationErrors: string[] = [];
  @ViewChild('requestFrom') requestForm: NgForm | undefined;

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.requestForm?.dirty) {
      $event.returnValue = true;
    }
  }
  request() {
  }
}
