import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {UserProfile} from "../_model/userProfile.model";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getUserProfile(email: string){
    return this.http.get<UserProfile>(this.baseUrl + 'user/artist/' + email);
  }
}
