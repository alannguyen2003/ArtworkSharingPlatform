import { Component } from '@angular/core';
import {Router} from "@angular/router";
import {User} from "../../_model/user.model";
import {AccountService} from "../../_services/account.service";
import {take} from "rxjs";

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.css']
})
export class ProfileEditComponent {
  user : User | undefined;
  constructor(private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user;
      }
    });
  }


}
