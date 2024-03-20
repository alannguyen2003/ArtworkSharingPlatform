import { Package } from 'src/app/_model/package.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ManagerService } from 'src/app/_services/manager.service';

@Component({
  selector: 'app-package-detail',
  templateUrl: './package-detail.component.html',
  styleUrls: ['./package-detail.component.css']
})
export class PackageDetailComponent implements OnInit {
  package: Package;

  constructor(private managerService: ManagerService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getPackageDetail();
  }

  getPackageDetail(): void {
    const packageId = +this.route.snapshot.paramMap.get('id')!;
    this.managerService.getPackageById(packageId).subscribe((packageData) => {
      this.package = packageData;
    });
  }
}
