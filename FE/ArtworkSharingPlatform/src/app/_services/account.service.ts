import { Injectable } from '@angular/core';
import {BehaviorSubject, map} from "rxjs";
import {User} from "../_model/user.model";
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {PresenceService} from "./presence.service";
import {DecodedToken} from "../_model/decodeToken.model";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient,
              private toastr: ToastrService,
              private router: Router,
              private presenceService: PresenceService) { }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'auth/login', model)
      .pipe(
        map((response: User) => {
          const user = response;
          if (user) {
            this.setCurrentUser(user);
          }
        })
      );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + "auth/register", model)
      .pipe(
        map(user => {
          if (user) {
            this.setCurrentUser(user);
          }
        })
      );
  }

  setCurrentUser(user: User) {
    const expiry = this.getDecodedToken(user.token).exp;
    const now = Math.floor(new Date().getTime() / 1000);
    if (expiry < now) {
      localStorage.removeItem('user');
      this.router.navigateByUrl('/login');
    } else {
      user.roles = [];
      const roles = this.getDecodedToken(user.token).role;
      Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
      localStorage.setItem('user', JSON.stringify(user));
      this.currentUserSource.next(user);
      this.presenceService.createHubConnection(user);
    }
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.presenceService.stopHubConnection();
  }

  getDecodedToken(token: string) : DecodedToken{
    return JSON.parse(atob(token.split(".")[1]));
  }

  getProfile() {
    return this.http.get<User>(this.baseUrl + 'user/get-profile');
  }


}
