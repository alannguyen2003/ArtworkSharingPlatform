import { Component } from '@angular/core';
import {CommissionHistoryAdmin} from "../../../../_model/commissionHistoryAdmin.model";
import {AdminService} from "../../../../_services/admin.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-detail-commission',
  templateUrl: './detail-commission.component.html',
  styleUrls: ['./detail-commission.component.css']
})
export class DetailCommissionComponent {
  commission: CommissionHistoryAdmin;

  constructor(
      private adminService: AdminService,
      private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const commissionId = +params['id'];
      this.adminService.getSingleCommission(commissionId).subscribe(commission => {
        this.commission = commission;
      });
    });
  }
}
