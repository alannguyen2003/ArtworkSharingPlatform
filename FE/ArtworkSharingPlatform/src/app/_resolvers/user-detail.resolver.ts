import { ResolveFn } from '@angular/router';
import {inject} from "@angular/core";
import {UserService} from "../_services/user.service";
import {UserProfile} from "../_model/userProfile.model";

export const userDetailResolver: ResolveFn<UserProfile> = (route, state) => {
  var userService = inject(UserService);
  return userService.getUserProfile(route.paramMap.get('email')!);
};
