import { Component } from '@angular/core';
import {AccountService} from "../../_services/account.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  model: any = {};
  validationErrs: string[] = [];
  constructor(private accountService: AccountService, private router: Router) {
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: _ => this.router.navigateByUrl('/'),
      error: err => this.validationErrs = err
    });
  }
}
