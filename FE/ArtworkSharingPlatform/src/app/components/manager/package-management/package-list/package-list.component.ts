// src/app/components/manager/package-management/package-list/package-list.component.ts
import { Component, OnInit } from '@angular/core';
import { ManagerService } from 'src/app/_services/manager.service';
import { Package } from 'src/app/_model/package.model';
import Swal from 'sweetalert2';

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

  deletePackage(packageId: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.managerService.deletePackage(packageId).subscribe({
          next: () => {
            Swal.fire(
              'Deleted!',
              'The package has been deleted.',
              'success'
            );
            // Refresh the artworks list
            this.loadAllPackages();
          },
          error: (error) => {
            Swal.fire(
              'Failed!',
              'There was an error deleting the package.',
              'error'
            );
          }
        });
      }
    });
  }
}
