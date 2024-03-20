import {Component, HostListener, OnInit, ViewChild} from '@angular/core';
import {Router} from "@angular/router";
import {User} from "../../_model/user.model";
import {AccountService} from "../../_services/account.service";
import {take} from "rxjs";
import {UserService} from "../../_services/user.service";
import {ToastrService} from "ngx-toastr";
import {NgForm} from "@angular/forms";
import {LocalStorageService} from "../../_services/local-storage.service";
import {UserInfo} from "../../_model/userInfo.model";

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent implements OnInit{
  user : User | undefined;
  validationErrors: string[] = [];
  @ViewChild('editForm') editForm: NgForm | undefined;
  userInfo: UserInfo;
  @HostListener('window:beforeunload', ['$event']) unloadNotification($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }
  }
  constructor(private accountService: AccountService,
              private userService: UserService,
              private toastr: ToastrService,
              private localStorage: LocalStorageService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user;
      }
    });
    this.userService.getRemainingCredits(this.user?.email).subscribe(response => {
      this.userInfo = response;
      console.log(this.userInfo)
    })
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
        this.editForm?.reset(this.user);
      },
      error: errs => this.validationErrors = errs
    });
  }


}
