import { Component } from '@angular/core';
import {UserProfile} from "../../../_model/userProfile.model";
import {User} from "../../../_model/user.model";
import {ActivatedRoute} from "@angular/router";
import {AccountService} from "../../../_services/account.service";
import {UserService} from "../../../_services/user.service";
import {ToastrService} from "ngx-toastr";
import {take} from "rxjs";
import {Follow} from "../../../_model/follow.model";

@Component({
  selector: 'app-profile-banner',
  templateUrl: './profile-banner.component.html',
  styleUrls: ['./profile-banner.component.css']
})
export class ProfileBannerComponent {
  userProfile: UserProfile | undefined;
  user: User | undefined;
  checkIsFollowed = false;
  constructor(private route: ActivatedRoute,
              private accountService: AccountService,
              private userService: UserService,
              private toastr: ToastrService) {
  }
  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.userProfile = data['userProfile'];
      }
    });
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user;
      }
    });
    if (this.userProfile && this.user) {
      this.checkIsFollowed = this.userProfile.isFollowedByUsers.some(r => r.sourceUserEmail == this.user?.email);
    }
  }

  follow() {
    if (!this.userProfile) return;
    this.userService.folowUser(this.userProfile.email).subscribe({
      next: _ => {
        var index = this.userProfile?.isFollowedByUsers.findIndex(x => x.sourceUserEmail == this.user?.email);

        if (index! >= 0) {
          this.userProfile?.isFollowedByUsers.splice(index!, 1);
          this.toastr.success('Unfollow Successfully');
        }
        else {
          this.userProfile?.isFollowedByUsers.push({targetUserEmail : this.userProfile?.email, sourceUserEmail: this.user?.email} as Follow)
          this.toastr.success('Follow Successfully');
        }
        this.checkIsFollowed = !this.checkIsFollowed;
      }
    });
  }
}
