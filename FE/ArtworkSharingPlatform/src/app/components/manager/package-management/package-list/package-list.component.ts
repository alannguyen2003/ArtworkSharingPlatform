// src/app/components/manager/package-management/package-list/package-list.component.ts
import { Component, OnInit } from '@angular/core';
import { ManagerService } from 'src/app/_services/manager.service';
import { Package } from 'src/app/_model/package.model';

@Component({
  selector: 'app-package-list',
  templateUrl: './package-list.component.html',
  styleUrls: ['./package-list.component.css']
})
export class PackageListComponent implements OnInit {
  packages: Package[] = [];

  constructor(private managerService: ManagerService) { }

  ngOnInit(): void {
    this.loadAllPackages();
  }

  loadAllPackages() {
    this.managerService.getAllPackages().subscribe(packages => {
      this.packages = packages;
    });
  }
}
