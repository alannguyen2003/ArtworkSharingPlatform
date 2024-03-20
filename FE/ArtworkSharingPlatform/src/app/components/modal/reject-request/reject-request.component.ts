import {Component, ViewChild} from '@angular/core';
import {NgForm} from "@angular/forms";
import {Artwork} from "../../../_model/artwork.model";
import {environment} from "../../../../environments/environment";
import {Report} from "../../../_model/report.model";
import {BsModalRef} from "ngx-bootstrap/modal";
import {ArtworkService} from "../../../_services/artwork.service";
import {ToastrService} from "ngx-toastr";
import {RejectRequest} from "../../../_model/rejectRequest.model";
import {CommissionHistoryAdmin} from "../../../_model/commissionHistoryAdmin.model";
import {CommissionService} from "../../../_services/commission.service";

@Component({
  selector: 'app-reject-request',
  templateUrl: './reject-request.component.html',
  styleUrls: ['./reject-request.component.css']
})
export class RejectRequestComponent {
  @ViewChild('rejectForm') rejectForm: NgForm | undefined;
  commission: CommissionHistoryAdmin | undefined;
  baseUrl = environment.apiUrl;
  rejectRequest: RejectRequest = {} as RejectRequest;
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
              private toast: ToastrService,
              private commissionService: CommissionService) {
  }
  rejectRequestSubmit() {
    if (!this.commission) return;
    this.validationErrors = [];
    this.rejectRequest.commissionRequestId = <number>this.commission?.id;
    this.commissionService.rejectCommission(this.rejectRequest).subscribe({
      next: _ => {
        this.toast.success("Reject successfully!");
      },
      error: errs => this.validationErrors = errs
    });

  }
}
