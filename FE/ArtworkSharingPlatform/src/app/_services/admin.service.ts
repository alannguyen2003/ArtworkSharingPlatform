import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { UserAdmin } from '../_model/userAdmin.model';
import { Artwork } from '../_model/artwork.model';
import { Observable } from 'rxjs';
import { ArtworkAdminDTO } from '../_model/artworkAdminDTO.model';
import { ConfigManagerRequest } from '../_model/configManagerRequest.model';
import { CommissionHistoryAdmin } from '../_model/commissionHistoryAdmin.model';
import { ReportDTO } from '../_model/reportDTO.model';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}

  getAllUsers() {
    return this.http.get<UserAdmin[]>(this.baseUrl + 'admin/User');
  }
  getUserByEmail(email: string) {
    return this.http.get<UserAdmin>(
      this.baseUrl + 'admin/GetUserEmail/' + email
    );
  }
  createUserAdmin(userDto: UserAdmin) {
    return this.http.post<UserAdmin>(
      `${this.baseUrl}admin/CreateUser`,
      userDto
    );
  }
  updateUserAdmin(userDto: UserAdmin){
    return this.http.put<UserAdmin>(`${this.baseUrl}admin/UpdateUser`, userDto);
  }
  deleteUserAdminByEmail(email: string) {
    return this.http.delete(`${this.baseUrl}admin/DeleteUser/${email}`);
  }
  getAllArtworks(): Observable<ArtworkAdminDTO[]> {
    return this.http.get<ArtworkAdminDTO[]>(`${this.baseUrl}admin/Artworks`);
  }
  deleteArtwork(artworkId: number) {
    return this.http.delete(`${this.baseUrl}admin/${artworkId}`);
  }
  getAllCommissions(): Observable<CommissionHistoryAdmin[]>{
    return this.http.get<CommissionHistoryAdmin[]>(`${this.baseUrl}admin/Commissions`);
  }
  getSingleCommission(commissionId: number): Observable<CommissionHistoryAdmin> {
    return this.http.get<CommissionHistoryAdmin>(`${this.baseUrl}admin/${commissionId}`);
  }
  getAllReports(): Observable<ReportDTO[]> {
    return this.http.get<ReportDTO[]>(`${this.baseUrl}admin/report`);
  }
  getReportDetail(reportId: number): Observable<ReportDTO> {
    return this.http.get<ReportDTO>(`${this.baseUrl}admin/reportDetail/${reportId}`);
  } 
  getAllConfig(): Observable<ConfigManagerRequest[]> {
    return this.http.get<ConfigManagerRequest[]>(`${this.baseUrl}admin/config`);
  } 
  getSingleConfig(configId: number): Observable<ConfigManagerRequest> {
    return this.http.get<ConfigManagerRequest>(`${this.baseUrl}admin/config/${configId}`);
  }  
}
