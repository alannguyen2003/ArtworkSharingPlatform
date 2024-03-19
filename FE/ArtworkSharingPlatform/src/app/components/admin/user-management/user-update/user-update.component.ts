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

  constructor(
    private adminService: AdminService,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private router: Router
  ) {
    this.updateUserForm = this.fb.group({
      id: ['', Validators.required],
      email: [{ value: '', disabled: true }, Validators.required],
      name: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
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

  updateUser() {
    if (!this.updateUserForm.valid) {
      const formData = this.updateUserForm.getRawValue();
      // Tạo đối tượng UserAdminDTO từ dữ liệu form và bao gồm userImageUrl từ this.user
      const userDto: UserAdmin = {
        id: formData.id,
        name: formData.name,
        phoneNumber: formData.phoneNumber,
        email: formData.email, // Email không thay đổi nhưng vẫn cần gửi lại cho server
        description: formData.description,
        status: formData.status,
        role: +formData.role, // Chú ý: Đảm bảo rằng giá trị của role là số
        remaningCredit: formData.remaningCredit,
        packageId: formData.packageId,
        userImageUrl: this.user.userImageUrl, // Sử dụng giá trị từ this.user
      };
  
      this.adminService.updateUserAdmin(userDto).subscribe({
        next: () => {
          this.router.navigate(['/admin/user-list']); // Điều hướng về trang danh sách người dùng sau khi cập nhật thành công
        },
        error: (error) => {
          console.error('Update user failed: ', error);
        },
      });
    }
  }
  
}
