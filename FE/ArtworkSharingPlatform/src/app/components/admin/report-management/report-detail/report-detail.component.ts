import { ReportDTO } from 'src/app/_model/reportDTO.model';
import { AdminService } from 'src/app/_services/admin.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-report-detail',
  templateUrl: './report-detail.component.html',
  styleUrls: ['./report-detail.component.css'],
})
export class ReportDetailComponent implements OnInit {
  report: ReportDTO;

  constructor(
    private adminService: AdminService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getReportDetail();
  }

  getReportDetail(): void {
    const reportId = +this.route.snapshot.paramMap.get('id')!;
    this.adminService.getReportDetail(reportId).subscribe((report) => {
      this.report = report;
    });
  }
}
