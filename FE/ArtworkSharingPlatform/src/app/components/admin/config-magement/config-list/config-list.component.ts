import { ConfigManagerRequest } from 'src/app/_model/configManagerRequest.model';
import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../../_services/admin.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-config-list',
  templateUrl: './config-list.component.html',
  styleUrls: ['./config-list.component.css']
})
export class ConfigListComponent implements OnInit{
  configs: ConfigManagerRequest[] = [];

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.loadAllConfigs();
  }

  loadAllConfigs() {
    this.adminService.getAllConfig().subscribe((configs) => {
      this.configs = configs;
    });
  }
}
