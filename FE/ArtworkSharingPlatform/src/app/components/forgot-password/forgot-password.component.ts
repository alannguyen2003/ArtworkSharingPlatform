import { Component } from '@angular/core';
import {UserService} from "../../_services/user.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {
  emailForgot: string = '';
  model: any = {};

  constructor(private userService: UserService,
              private toastrService: ToastrService) {
  }

  onForgotPasswordRequest() {
    this.userService.forgotPassword(this.emailForgot).subscribe({
      next: _ => {
        this.toastrService.success("The code have been sent to your email");
      }
    });
  }

  onResetPasswordRequest() {
    if (!this.emailForgot) return;
    this.model.email = this.emailForgot;
    this.userService.resetPassword(this.model).subscribe({
      next: _ => {
        this.toastrService.success("Your password have been reset");
      }
    });
  }
}
