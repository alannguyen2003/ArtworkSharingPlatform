import { Component } from '@angular/core';
import {CommissionHistoryAdmin} from "../../../../_model/commissionHistoryAdmin.model";
import {AdminService} from "../../../../_services/admin.service";
import {ActivatedRoute} from "@angular/router";
import {ReportModalComponent} from "../../../modal/report-modal/report-modal.component";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {RejectRequestComponent} from "../../../modal/reject-request/reject-request.component";
import { CommissionService } from '../../../../_services/commission.service';
import { AcceptRequest } from 'src/app/_model/acceptRequest.model';

@Component({
  selector: 'app-detail-commission',
  templateUrl: './detail-commission.component.html',
  styleUrls: ['./detail-commission.component.css']
})
export class DetailCommissionComponent {
  commission: CommissionHistoryAdmin;
  actualPrice: number;
  isAccepted: boolean = false;
  isPending: boolean = false;
  isActualPrice: boolean = false;
  bsModalRef: BsModalRef<RejectRequestComponent> = new BsModalRef<RejectRequestComponent>();
    constructor(
    private adminService: AdminService,
    private route: ActivatedRoute,
    private commissionService: CommissionService,
    private modalService: BsModalService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const commissionId = +params['id'];
      this.adminService.getSingleCommission(commissionId).subscribe(commission => {
        this.commission = commission;
        console.log(this.commission)
        if (this.commission.commissionStatus == 'Pending') this.isPending = true;
        if (this.commission.commissionStatus != 'Pending') this.isAccepted = true;
        this.isActualPrice = this.commission.actualPrice > 0;
      });
    });
  }
  openReportModal() {
    const config = {
      class: 'modal-dialog-centered modal-lg',
      initialState: {
        commission: this.commission
      }
    };
    this.bsModalRef = this.modalService.show(RejectRequestComponent, config);
  }
  acceptCommission() {
    let commissionRequest = {
      commissionRequestId: this.commission.id,
      actualPrice: this.actualPrice
    }
    this.commissionService.acceptCommission(commissionRequest).subscribe(
      params => {
        console.log(params)
      }
    )
  }
}
