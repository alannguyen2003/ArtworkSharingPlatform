import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {RequestArtwork} from "../_model/request-artwork.model";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class CommissionService {
  baseUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }

  addCommission(requestArtwork: RequestArtwork) {
    return this.http.post(this.baseUrl + 'Commission/Create', requestArtwork);
  }
}
