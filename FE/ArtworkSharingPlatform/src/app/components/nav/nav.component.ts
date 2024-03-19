import {Component, ElementRef, OnDestroy, OnInit} from '@angular/core';
import {Location} from "@angular/common";
import {AccountService} from "../../_services/account.service";
import {Router} from "@angular/router";
import {Config} from "../../_model/config.model";
import {ConfigService} from "../../_services/config.service";
import {UserService} from "../../_services/user.service";
import {UserInfo} from "../../_model/userInfo.model";
import {take} from "rxjs";
import {User} from "../../_model/user.model";
import {LocalStorageService} from "../../_services/local-storage.service";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  userInfo: UserInfo;
  user: User | undefined;
  isArtist: boolean = false;
  constructor(public accountService: AccountService,
              private userService: UserService,
              private router: Router,
              private localStorage: LocalStorageService) {
    this.user = localStorage.getDataFromLocal("user");
    this.isArtist = this.user?.roles[1] == "Artist";
    this.userService.getRemainingCredits(this.user?.email).subscribe(response => {
      this.userInfo = response;
      console.log(this.userInfo)
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
