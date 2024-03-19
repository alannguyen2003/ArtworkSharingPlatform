import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {Package} from "../_model/package.model";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PackageService {
  baseUrl = environment.apiUrl;
  packages: Package[] = [];
  constructor(private http: HttpClient) { }

  loadPackages(){
    return this.http.get<Package[]>(this.baseUrl + 'Manager/packages');
  }
}
