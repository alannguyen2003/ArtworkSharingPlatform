import { Component } from '@angular/core';
import {CommissionHistoryAdmin} from "../../../_model/commissionHistoryAdmin.model";
import {Subject} from "rxjs";
import {AdminService} from "../../../_services/admin.service";
import {CommissionHistoryAudience} from "../../../_model/commissionHistoryAudience.model";
import {CommissionService} from "../../../_services/commission.service";

@Component({
  selector: 'app-commission-artist',
  templateUrl: './commission-artist.component.html',
  styleUrls: ['./commission-artist.component.css']
})
export class CommissionArtistComponent {
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
    this.commissionService.getCommissionsArtist().subscribe(response => {
      this.commissionsItem = response;
      this.dtTrigger.next(null);
    });
  }
}
