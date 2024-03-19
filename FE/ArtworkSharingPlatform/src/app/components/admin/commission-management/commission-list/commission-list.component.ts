import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../../_services/admin.service';
import { CommissionHistoryAdmin } from '../../../../_model/commissionHistoryAdmin.model';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-commission-list',
  templateUrl: './commission-list.component.html',
  styleUrls: ['./commission-list.component.css'],
})
export class CommissionListComponent implements OnInit {
  commissions: CommissionHistoryAdmin[] = [];
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
   }
   dtTrigger: Subject<any> = new Subject<any>();

  constructor(private adminService: AdminService) {}

  ngOnInit() {
    this.loadAllCommissions();
  }

  loadAllCommissions() {
    this.adminService.getAllCommissions().subscribe((commissions) => {
      this.commissions = commissions;
      this.dtTrigger.next(null);
    });
  }
}
