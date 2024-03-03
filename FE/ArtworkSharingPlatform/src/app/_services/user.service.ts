import { Injectable } from '@angular/core';
import {UserParam} from "../_model/userParam.model";
import {AccountService} from "./account.service";
import {BehaviorSubject, take} from "rxjs";
import {User} from "../_model/user.model";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  fakeUserParams: UserParam = {
    totalItemPerPage: 6,
    rowSize: 3,
    orderBy: 'lowPrice'
  };
  private currentUserParamSource = new BehaviorSubject<UserParam | null>(null);
  currentUserParam$ = this.currentUserParamSource.asObservable();
  constructor(private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next:user => {
        if (user) {
          this.currentUserParamSource.next(this.fakeUserParams) ;
        }
      }
    });
  }
  setUserParams(params: UserParam) {
    this.currentUserParamSource.next(params);
  }
}
