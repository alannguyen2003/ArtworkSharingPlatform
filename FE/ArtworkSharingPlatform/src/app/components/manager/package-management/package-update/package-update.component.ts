import { Message } from './../../../../_model/message.model';
import { Package } from 'src/app/_model/package.model';
// Trong package-update.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ManagerService } from 'src/app/_services/manager.service';

@Component({
  selector: 'app-package-update',
  templateUrl: './package-update.component.html',
  styleUrls: ['./package-update.component.css'],
})
export class PackageUpdateComponent implements OnInit {
  updatePackageForm: FormGroup;
  package: Package = {} as Package;
  packageId: number;
  errorMessage: string = '';

  constructor(
    private managerService: ManagerService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.updatePackageForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      credit: ['', [Validators.required, Validators.min(0)]],
      price: ['', [Validators.required, Validators.min(0)]],
      status: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      const id = params['id'];
      this.managerService.getPackageById(id).subscribe((Package) => {
        this.package = Package;
        this.updatePackageForm.patchValue({
          id: Package.id,
          name: Package.name,
          credit: Package.credit,
          price: Package.price,
          status: Package.status
        });
      });
    });
  }

  getPackageDetail(): void {
    const packageId = +this.route.snapshot.paramMap.get('id')!;
    this.managerService.getPackageById(packageId).subscribe((packageData) => {
      this.package = packageData;
    });
  }

  onUpdate(): void {
    if (this.updatePackageForm.valid) {
      const packageData: Package = this.updatePackageForm.value;
      this.managerService.updatePackage(packageData).subscribe({
        next: () => {
          this.router.navigate(['/manager/package-management/package-list']);
        },
        error: (error) => {
          this.router.navigate(['/manager/package-management/package-list']);
        },
      });
    }else{
      this.errorMessage = 'Form is not valid. Please check your input.';
    }
  }
}
