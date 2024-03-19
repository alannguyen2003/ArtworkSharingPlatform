import { UserAdmin } from 'src/app/_model/userAdmin.model';
import { AdminService } from './../../../../_services/admin.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent {
  user: UserAdmin = {
    id: 0,
    name: '',
    phoneNumber: '',
    email: '',
    description: '',
    status: 0,
    role: 0,
    remaningCredit: 0,
    packageId: 0,
    userImageUrl: '',
  };

  constructor(
    private adminService: AdminService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      const userEmail = params['email'];
      this.adminService.getUserByEmail(userEmail).subscribe(user => {
        this.user = user;
      });
    });
  }
}
