import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Config} from "../_model/config.model";
import {environment} from "../../environments/environment";
import {BehaviorSubject} from "rxjs";
import {User} from "../_model/user.model";

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  baseUrl = environment.apiUrl;
  config: Config | undefined;
  constructor(private http: HttpClient) {
    this.loadConfig();
  }
  getConfig() {
    return this.http.get<Config>(this.baseUrl + 'configmanager/lastestconfig');
  }
  loadConfig() {
    return this.http.get<Config>(this.baseUrl + 'configmanager/lastestconfig').subscribe({
      next: config => {
        this.config = config;
      }
    });
  }
}
