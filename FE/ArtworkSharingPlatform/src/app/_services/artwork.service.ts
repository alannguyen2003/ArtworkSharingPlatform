import { Injectable } from '@angular/core';
import {Artwork} from "../_model/artwork.model";
import {User} from "../_model/user.model";
import {HttpClient} from "@angular/common/http";
import {AccountService} from "./account.service";
import {map, of, take} from "rxjs";
import {UserParams} from "../_model/userParams.model";
import {getPaginatedResult, getPaginationHeaders} from "./pagination-helper.service";
import {environment} from "../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class ArtworkService {
  baseUrl = environment.apiUrl;
  user: User | undefined;
  userParams: UserParams | undefined;
  fakeUserParams: UserParams = {
    minPrice: 0,
    maxPrice: 10000000,
    orderBy: 'lowPrice',
    pageNumber: 1,
    pageSize: 6
  };
  artworkCache = new Map();
  artworks: Artwork[] = [];
  constructor(private http: HttpClient, private accountService: AccountService)
  {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.user = user;
          this.userParams = this.fakeUserParams;
        }
      }
    })
  }

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  resetUserParams() {
    if (this.user) {
      this.userParams = this.fakeUserParams;
      return this.userParams;
    }
    return;
  }


  getArtworks(userParams: UserParams) {
    const response = this.artworkCache.get(Object.values(userParams).join('-'));
    if(response) return of(response);

    if (userParams.pageSize == null) userParams.pageSize = this.fakeUserParams.pageSize;
    let params = getPaginationHeaders(userParams.pageNumber, userParams.pageSize);
    params = params.append('minPrice', userParams.minPrice);
    params = params.append('maxPrice', userParams.maxPrice);
    params = params.append('orderBy', userParams.orderBy);
    return getPaginatedResult<Artwork[]>(this.baseUrl + 'artworks', params, this.http).pipe(
      map(response => {
        this.artworkCache.set(Object.values(userParams).join('-'), response);
        return response;
      })
    );
  }
}
