import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { RequestArtwork } from "../_model/request-artwork.model";
import { environment } from "../../environments/environment";
import { CommissionHistoryAdmin } from "../_model/commissionHistoryAdmin.model";
import { CommissionHistoryAudience } from "../_model/commissionHistoryAudience.model";
import { RejectRequest } from "../_model/rejectRequest.model";
import { AcceptRequest } from '../_model/acceptRequest.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommissionService {
  baseUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) { }

  addCommission(requestArtwork: RequestArtwork) {
    return this.http.post(this.baseUrl + 'Commission/Create', requestArtwork);
  }

  getCommissions() {
    return this.http.get<CommissionHistoryAudience>(this.baseUrl + "Commission/Sender/GetAll");
  }
  getCommissionsArtist() {
    return this.http.get<CommissionHistoryAudience>(this.baseUrl + "Commission/Receiver/GetAll");
  }
  acceptCommission(acceptCommissionRequest: AcceptRequest) {
    const url = `${this.baseUrl}Commission/Accept`
    console.log('url: ' + url)
    return this.http.post(url, acceptCommissionRequest)
  }
  getSingleCommission(commissionId: number): Observable<CommissionHistoryAdmin> {
    return this.http.get<CommissionHistoryAdmin>(`${this.baseUrl}commission/${commissionId}`);
  }

  rejectCommission(rejectRequest: RejectRequest) {
    return this.http.post(this.baseUrl + "Commission/Reject", rejectRequest);
  }
}
