import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {UserProfile} from "../_model/userProfile.model";
import {User} from "../_model/user.model";
import {UserImage} from "../_model/userImage.model";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getUserProfile(email: string){
    return this.http.get<UserProfile>(this.baseUrl + 'user/artist/' + email);
  }
  updateProfile(user: User) {
    return this.http.put(this.baseUrl + 'user/edit-profile', user);
  }

  folowUser(email: string) {
    return this.http.post(this.baseUrl + 'artworks/follow/' + email, {});
  }
  deleteImageDuringChangeAvatar(userImage: UserImage) {
    return this.http.delete(this.baseUrl + 'image/user-image', {
      body: userImage
    });
  }

  changeAvatar(userImage: UserImage) {
    return this.http.put(this.baseUrl+ 'user/change-avatar', userImage);
  }
}
