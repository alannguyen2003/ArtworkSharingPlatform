import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../../../../_services/admin.service';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.css']
})
export class UserCreateComponent implements OnInit {
  createUserForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private adminService: AdminService,
    private router: Router
  ) {
    this.createUserForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(100)]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      email: ['', [Validators.required, Validators.email]],
      description: ['', [Validators.required, Validators.minLength(1), Validators.maxLength(100)]],
      role: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
  }

  onSubmit(): void {
    if (this.createUserForm.valid) {
      this.adminService.createUserAdmin(this.createUserForm.value).subscribe({
        next: () => {
          this.router.navigate(['/admin/user-management/user-list']);
          // Display success message
        },
        error: error => {
          // Display error message
          console.error('There was an error!', error);
          this.router.navigate(['/admin/user-management/user-list']);
        }
      });
    }
  }
}
