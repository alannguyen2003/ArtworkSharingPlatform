// Thêm vào các import cần thiết ở đầu file
import { AdminService } from 'src/app/_services/admin.service';
import { ReportDTO } from '../../../../_model/reportDTO.model';
import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.css'],
})
export class ReportListComponent implements OnInit {
  reports: ReportDTO[] = [];
  
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
   }
   dtTrigger: Subject<any> = new Subject<any>();

  constructor(private adminService: AdminService) {}

  ngOnInit() {
    this.loadAllReports();
  }

  loadAllReports() {
    this.adminService.getAllReports().subscribe(reports => {
      this.reports = reports;
      this.dtTrigger.next(null);
    });
  }
}
