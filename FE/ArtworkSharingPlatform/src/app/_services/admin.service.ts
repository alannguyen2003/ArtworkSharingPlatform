import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { UserAdmin } from '../_model/userAdmin.model';
import { Artwork } from '../_model/artwork.model';
import { Observable } from 'rxjs';
import { ArtworkAdminDTO } from '../_model/artworkAdminDTO.model';
import { ConfigManagerRequest } from '../_model/configManagerRequest.model';
import { CommissionHistoryAdmin } from '../_model/commissionHistoryAdmin.model';

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
  updateUserAdmin(userDto: UserAdmin): Observable<UserAdmin> {
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
    return this.http.get<CommissionHistoryAdmin>(`${this.baseUrl}admin/Commissions/${commissionId}`);
  }
  createConfig(newConfig: ConfigManagerRequest): Observable<any> {
    return this.http.post(`${this.baseUrl}api/ConfigManager/Create`, newConfig);
  }
}
