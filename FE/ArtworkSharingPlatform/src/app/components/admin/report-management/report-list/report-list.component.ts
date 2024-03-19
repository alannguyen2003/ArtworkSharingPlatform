import { Component } from '@angular/core';
import { ReportDTO } from 'src/app/_model/reportDTO.model';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.css']
})
export class ReportListComponent {
  reports: ReportDTO[] = [];

  constructor(private adminService: AdminService) { }

  ngOnInit() {
    this.loadAllReports();
  }

  loadAllReports() {
    this.adminService.getAllReports().subscribe((reports) => {
      this.reports = reports;
    });
  }
}
