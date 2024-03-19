import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../../_services/admin.service';
import { CommissionHistoryAdmin } from '../../../../_model/commissionHistoryAdmin.model';

@Component({
  selector: 'app-commission-list',
  templateUrl: './commission-list.component.html',
  styleUrls: ['./commission-list.component.css'],
})
export class CommissionListComponent implements OnInit {
  commissions: CommissionHistoryAdmin[] = [];

  constructor(private adminService: AdminService) {}

  ngOnInit() {
    this.loadAllCommissions();
  }

  loadAllCommissions() {
    this.adminService.getAllCommissions().subscribe((commissions) => {
      this.commissions = commissions;
    });
  }
}
