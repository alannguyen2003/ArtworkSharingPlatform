import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {User} from "../_model/user.model";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private fakeUser: User = {
    name: 'Test',
    telephone: '0123456789',
    email: 'test@gmail.com',
    imageUrl: 'https://randomuser.me/api/portraits/men/2.jpg',
    roles: ['Audience', 'Artist']
  } as User;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  constructor() { }

  login() {
    this.setCurrentUser(this.fakeUser);
  }

  setCurrentUser(user: User){
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }


}
