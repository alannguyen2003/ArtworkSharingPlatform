import { Component } from '@angular/core';
import {CommissionHistoryAdmin} from "../../../../_model/commissionHistoryAdmin.model";
import {AdminService} from "../../../../_services/admin.service";
import {ActivatedRoute} from "@angular/router";
import {CommissionHistoryAudience} from "../../../../_model/commissionHistoryAudience.model";

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent {
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
