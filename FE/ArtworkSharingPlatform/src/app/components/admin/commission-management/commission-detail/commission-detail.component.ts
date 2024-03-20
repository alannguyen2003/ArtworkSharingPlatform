
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminService } from 'src/app/_services/admin.service';
import { CommissionHistoryAdmin } from 'src/app/_model/commissionHistoryAdmin.model';

@Component({
  selector: 'app-commission-detail',
  templateUrl: './commission-detail.component.html',
  styleUrls: ['./commission-detail.component.css']
})
export class CommissionDetailComponent implements OnInit {
  commission: CommissionHistoryAdmin;

  constructor(
    private adminService: AdminService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const commissionId = +params['commissionId'];
      this.adminService.getSingleCommission(commissionId).subscribe(commission => {
        this.commission = commission;
      });
    });
  }
}
