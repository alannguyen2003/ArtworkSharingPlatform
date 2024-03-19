import { AdminService } from './../../../../_services/admin.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserAdmin } from 'src/app/_model/userAdmin.model';
import Swal from 'sweetalert2';
import {Subject} from "rxjs";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css'],
})
export class UserListComponent {
  users: UserAdmin[] = [];
  dtOptions: DataTables.Settings = {
   pagingType: 'full_numbers'
  }
  dtTrigger: Subject<any> = new Subject<any>();

  constructor(private adminService: AdminService, private router: Router) {}

  ngOnInit() {
    this.loadAllUsers();
  }

  loadAllUsers() {
    this.adminService.getAllUsers().subscribe((users) => {
      this.users = users;
      this.dtTrigger.next(null);
    });
  }

  editUser(userEmail: string) {
    this.router.navigate(['/edit-user', userEmail]);
  }

  deleteUser(email: string) {
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
        this.adminService.deleteUserAdminByEmail(email).subscribe({
          next: () => {
            Swal.fire('Deleted!', 'Your user has been deleted.', 'success');
            this.loadAllUsers(); // Reload the users list
          },
          error: (error) => {
            Swal.fire('Failed!', 'There was a problem deleting the user.', 'error');
          }
        });
      }
    });
  }
}
