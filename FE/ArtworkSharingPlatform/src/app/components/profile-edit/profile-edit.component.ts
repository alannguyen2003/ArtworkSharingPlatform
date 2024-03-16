import {Component, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {User} from "../../_model/user.model";
import {AccountService} from "../../_services/account.service";
import {take} from "rxjs";
import {UserService} from "../../_services/user.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent implements OnInit{
  user : User | undefined;
  validationErrors: string[] = [];
  constructor(private accountService: AccountService,
              private userService: UserService,
              private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user;
      }
    });
  }

  ngOnInit() {
    if (!this.user) return;
    this.accountService.getProfile().subscribe({
      next: profile => {
        this.user = profile;
      }
    });
  }

  editProfile() {
    if (!this.user) return;
    this.validationErrors = [];
    this.userService.updateProfile(this.user).subscribe({
      next: _ => {
        this.toastr.success("Edit successfully!");
        if (this.user) this.accountService.setCurrentUser(this.user);
      },
      error: errs => this.validationErrors = errs
    });
  }


}
