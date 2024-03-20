import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ConfigManagerRequest } from 'src/app/_model/configManagerRequest.model';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-config-detail',
  templateUrl: './config-detail.component.html',
  styleUrls: ['./config-detail.component.css']
})
export class ConfigDetailComponent implements OnInit {
  config: ConfigManagerRequest;

  constructor(
    private adminService: AdminService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadConfigDetail();
  }

  loadConfigDetail(): void {
    const configId = +this.route.snapshot.paramMap.get('configId')!;
    console.log(configId);
    this.adminService.getSingleConfig(configId).subscribe((config) => {
      this.config = config;
    });
  }
}
