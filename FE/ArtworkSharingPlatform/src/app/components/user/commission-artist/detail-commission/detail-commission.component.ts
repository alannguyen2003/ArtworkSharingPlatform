import { Component } from '@angular/core';
import { CommissionHistoryAdmin } from "../../../../_model/commissionHistoryAdmin.model";
import { AdminService } from "../../../../_services/admin.service";
import { ActivatedRoute } from "@angular/router";
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
  constructor(
    private adminService: AdminService,
    private route: ActivatedRoute,
    private commissionService: CommissionService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const commissionId = +params['id'];
      this.adminService.getSingleCommission(commissionId).subscribe(commission => {
        this.commission = commission;
      });
    });
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
