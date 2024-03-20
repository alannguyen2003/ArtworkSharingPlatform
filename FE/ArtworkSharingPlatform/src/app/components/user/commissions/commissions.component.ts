import { Component } from '@angular/core';
import {CommissionHistoryAdmin} from "../../../_model/commissionHistoryAdmin.model";
import {Subject} from "rxjs";
import {AdminService} from "../../../_services/admin.service";
import {CommissionService} from "../../../_services/commission.service";
import {CommissionHistoryAudience} from "../../../_model/commissionHistoryAudience.model";

@Component({
  selector: 'app-commissions',
  templateUrl: './commissions.component.html',
  styleUrls: ['./commissions.component.css']
})
export class CommissionsComponent {
  commissionsItem: CommissionHistoryAudience;
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
  }
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private commissionService: CommissionService) {}

  ngOnInit() {
    this.loadAllCommissions();
  }

  loadAllCommissions() {
    this.commissionService.getCommissions().subscribe(response => {
      this.commissionsItem = response;
      this.commissionsItem.returnData.reverse();
      this.dtTrigger.next(null);
    });
  }
}
