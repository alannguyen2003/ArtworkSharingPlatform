import { Component } from '@angular/core';
import {CommissionHistoryAdmin} from "../../../../_model/commissionHistoryAdmin.model";
import {AdminService} from "../../../../_services/admin.service";
import {ActivatedRoute} from "@angular/router";
import {ReportModalComponent} from "../../../modal/report-modal/report-modal.component";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {RejectRequestComponent} from "../../../modal/reject-request/reject-request.component";

@Component({
  selector: 'app-detail-commission',
  templateUrl: './detail-commission.component.html',
  styleUrls: ['./detail-commission.component.css']
})
export class DetailCommissionComponent {
  commission: CommissionHistoryAdmin;
  actualPrice: number;
  bsModalRef: BsModalRef<RejectRequestComponent> = new BsModalRef<RejectRequestComponent>();

  constructor(
      private adminService: AdminService,
      private route: ActivatedRoute,
      private modalService: BsModalService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const commissionId = +params['id'];
      this.adminService.getSingleCommission(commissionId).subscribe(commission => {
        this.commission = commission;
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
}
