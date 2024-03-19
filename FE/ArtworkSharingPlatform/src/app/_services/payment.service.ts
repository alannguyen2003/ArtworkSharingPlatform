import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Package} from "../_model/package.model";
import {ISession} from "../_model/ISession";

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  loadSessionStripePayment(packageId: number){
    return this.http.get<ISession>(this.baseUrl + 'StripePayment/checkout?packageId=' + packageId);
  }
}
