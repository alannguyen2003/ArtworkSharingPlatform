import { Component } from '@angular/core';
import {UserImage} from "../../../_model/userImage.model";
import {ArtworkImage} from "../../../_model/artworkImage.model";
import {User} from "../../../_model/user.model";
import {AccountService} from "../../../_services/account.service";
import {take} from "rxjs";
import {UserService} from "../../../_services/user.service";

@Component({
  selector: 'app-change-avatar',
  templateUrl: './change-avatar.component.html',
  styleUrls: ['./change-avatar.component.css']
})
export class ChangeAvatarComponent {
  user: User | undefined;
  userImage: UserImage = {} as UserImage;
  constructor(private accountService: AccountService,
              private userService: UserService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.user = user;
          this.userImage.url = this.user.imageUrl
        }
      }
    });
  }
  onImageAdded(data: ArtworkImage) {
    if (this.userImage.publicId != null) {
      this.userService.deleteImageDuringChangeAvatar(this.userImage).subscribe({
        next : _ => console.log('Delete success')
      });
    }
    this.userImage.url = data.imageUrl;
    this.userImage.publicId = data.publicId;
  }

  saveAvatar() {
    if (!this.userImage) return;
    this.userService.changeAvatar(this.userImage).subscribe({
      next: _ => {
        if (this.user) {
          this.user.imageUrl = this.userImage.url;
          this.accountService.setCurrentUser(this.user);
        }
      }
    });
  }
}
