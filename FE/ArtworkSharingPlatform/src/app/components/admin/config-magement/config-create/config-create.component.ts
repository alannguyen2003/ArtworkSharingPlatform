import { AdminService } from './../../../../_services/admin.service';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-config-create',
  templateUrl: './config-create.component.html',
  styleUrls: ['./config-create.component.css'],
})
export class ConfigCreateComponent {
  configForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.configForm = this.fb.group({
      isGeneralConfig: false,
      logoUrl: '',
      myPhoneNumber: '',
      address: '',
      isPagingConfig: false,
      totalItemPerPage: '',
      rowSize: '',
      isAdvertisementConfig: false,
      companyName: '',
      companyPhoneNumber: '',
      companyEmail: '',
    });
  }

  isSectionActive(section: string): boolean {
    return this.configForm.get(section)?.value;
  }

  toggleSection(section: string): void {
    const control = this.configForm.get(section);
    if (control) {
      const status = control.value;
      control.setValue(!status);
    }
  }

  onSubmit() {
    this.adminService.createConfig(this.configForm.value).subscribe({
      next: () => {
        this.router.navigate(['/admin/config-management']);
      },
      error: (error) => {
        console.error('Failed to create config: ', error);
      },
    });
  }
}
