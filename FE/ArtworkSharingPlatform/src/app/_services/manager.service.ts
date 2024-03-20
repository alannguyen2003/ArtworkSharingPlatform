import { Transaction } from './../_model/transaction.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Package } from '../_model/package.model';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAllTransactions(): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.baseUrl}manager/transaction`);
  }
  getTransactionDetail(transactionId: number): Observable<Transaction> {
    return this.http.get<Transaction>(`${this.baseUrl}manager/transaction/${transactionId}`);
  }
  exportTransactionList(): Observable<Blob> {
    return this.http.get(`${this.baseUrl}manager/export/list`, { responseType: 'blob' });
  }  
  exportTransaction(transactionId: number): Observable<Blob> {
    return this.http.get(`${this.baseUrl}manager/export/${transactionId}`, { responseType: 'blob' });
  }
  getAllPackages(): Observable<Package[]> {
    return this.http.get<Package[]>(`${this.baseUrl}manager/packages`);
  }
}
