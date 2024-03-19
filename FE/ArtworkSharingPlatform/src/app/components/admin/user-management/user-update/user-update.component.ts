import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from '../../../../_services/admin.service';
import { UserAdmin } from 'src/app/_model/userAdmin.model';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.css'],
})
export class UserUpdateComponent implements OnInit {
  updateUserForm: FormGroup;
  user: UserAdmin = {} as UserAdmin;
  errorMessage: string = '';


  constructor(
    private adminService: AdminService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router,
    
  ) {
    this.updateUserForm = this.fb.group({
      id: ['', Validators.required],
      email: ['', Validators.required],
      name: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      description: [''],
      status: ['', Validators.required],
      role: ['', Validators.required],
      remaningCredit: ['', Validators.required],
      packageId: ['', Validators.required],
    });
  }

  ngOnInit() {
    this.route.params.subscribe((params) => {
      const email = params['email'];
      this.adminService.getUserByEmail(email).subscribe((user) => {
        this.user = user;
        console.log(user);
        this.updateUserForm.patchValue({
          id: user.id,
          email: user.email,
          name: user.name,
          phoneNumber: user.phoneNumber,
          description: user.description,
          status: user.status,
          role: user.role,
          remaningCredit: user.remaningCredit,
          packageId: user.packageId,
        });
      });
    });
  }

  onUpdate() {
    if (this.updateUserForm.valid) {
      const formData = this.updateUserForm.getRawValue();
      const userDto: UserAdmin = {
        id: formData.id,
        name: formData.name,
        phoneNumber: formData.phoneNumber,
        email: formData.email,
        description: formData.description,
        status: formData.status,
        role: this.convertRoleToNumber(formData.role),
        remaningCredit: formData.remaningCredit,
        packageId: formData.packageId,
        userImageUrl: '',
      };
  
      this.adminService.updateUserAdmin(userDto).subscribe({
        next: () => {
          this.router.navigate(['/admin/user-management/user-list']);
        },
        error: (error) => {
          this.errorMessage = 'Update user failed: ' + error;
        },
      });
    } else {
      // Xử lý trường hợp form không hợp lệ, có thể hiển thị thông báo lỗi
      this.errorMessage = 'Form is not valid. Please check your input.';
    }
  }
  
  private convertRoleToNumber(role: string): number {
    switch (role) {
      case 'Audience':
        return 0;
      case 'Artist':
        return 1;
      case 'Manager':
        return 2;
      case 'Admin':
        return 3;
      default:
        return -1; // Hoặc một giá trị mặc định phản ánh không khớp với bất kỳ vai trò nào
    }
  }
 
}
