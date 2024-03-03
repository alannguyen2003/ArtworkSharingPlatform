import {Component, ElementRef, OnDestroy, OnInit} from '@angular/core';
import {Location} from "@angular/common";
import {AccountService} from "../../_services/account.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  constructor(public accountService: AccountService, private router: Router) {
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
