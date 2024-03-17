import {Component, ViewChild} from '@angular/core';
import {BsModalRef} from "ngx-bootstrap/modal";
import {NgForm} from "@angular/forms";
import {environment} from "../../../../environments/environment";
import {ArtworkService} from "../../../_services/artwork.service";
import {Report} from "../../../_model/report.model";
import {Artwork} from "../../../_model/artwork.model";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-report-modal',
  templateUrl: './report-modal.component.html',
  styleUrls: ['./report-modal.component.css']
})
export class ReportModalComponent {
  @ViewChild('reportForm') reportForm: NgForm | undefined;
  artwork: Artwork | undefined;
  baseUrl = environment.apiUrl;
  report: Report = {} as Report;
  validationErrors : string[] = [];
  options: Object = {
    charCounterCount: true,
    charCounterMax: 200,
    imageUpload: true,
    imageUploadMethod: 'POST',
    imageUploadURL: this.baseUrl + 'image',
    events: {
      'image.beforeUpload': (images: any) => {
        console.log('before upload');
      },
      'image.uploaded': () => {
        console.log('uploaded');
      },
      'image.error': (error: any, response: any) => {
        console.log(error);
        // Response contains the original server response to the request if available.
      }
    }
  }
  constructor(public bsModalRef: BsModalRef,
              private artworkService: ArtworkService,
              private toast: ToastrService) {
  }
  reportSubmit() {
    if (!this.artwork) return;
    this.validationErrors = [];
    this.report.artworkId = this.artwork.id;
    this.artworkService.report(this.report).subscribe({
      next: _ => {
        this.toast.success("Report successfully!")
      },
      error: errs => this.validationErrors = errs
    });
  }
}
