import { Component } from '@angular/core';
import {PackageService} from "../../../_services/package.service";
import {Package} from "../../../_model/package.model";

@Component({
  selector: 'app-pricing',
  templateUrl: './pricing.component.html',
  styleUrls: ['./pricing.component.css']
})
export class PricingComponent {
  packages : Package[] = [];
  constructor(private packageService : PackageService) {
    this.packageService.loadPackages().subscribe(response => {
      this.packages = response;
      console.log(this.packages)
    });
  }
}
