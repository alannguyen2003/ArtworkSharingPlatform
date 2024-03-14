import { Injectable } from '@angular/core';
import {Artwork} from "../_model/artwork.model";
import {User} from "../_model/user.model";
import {HttpClient} from "@angular/common/http";
import {AccountService} from "./account.service";
import {map, of, take} from "rxjs";
import {UserParams} from "../_model/userParams.model";
import {getPaginatedResult, getPaginationHeaders} from "./pagination-helper.service";
import {Genre} from "../_model/genre.model";
import {environment} from "../../environments/environment";
import {ArtworkImage} from "../_model/artworkImage.model";

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

  getArtwork(id: number) {
    const artwork =[...this.artworkCache.values()]
      .reduce((arr, elm) => arr.concat(elm.result), []).find((artwork: Artwork) => artwork.id == id);
    if (artwork) return of(artwork);
    return this.http.get<Artwork>(this.baseUrl + 'artworks/' + id);
  }

  addArtwork(artwork: Artwork) {
    return this.http.post<Artwork>(this.baseUrl + 'artworks', artwork);
  }

  getGenreForArtwork() {
    return this.http.get<Genre[]>(this.baseUrl + 'genre');
  }

  likeArtwork(artworkId: number) {
    return this.http.post(this.baseUrl + 'artworks/like', artworkId);
  }

  confirmSell(artworkId: number) {
    return this.http.put(this.baseUrl + 'artworks/sell/' + artworkId, {});
  }

  getArtistArtwork() {
    return this.http.get<Artwork[]>(this.baseUrl + 'artworks/GetArtistArtwork');
  }
  updateArtworkInformation(artwork: Artwork) {
    return this.http.put(this.baseUrl + 'artworks', artwork);
  }

  setThumbNail(imageId: number) {
    return this.http.put(this.baseUrl + 'artworks/set-thumbnail/' + imageId, {});
  }

  addImageToArtwork(image: ArtworkImage) {
    return this.http.post<ArtworkImage>(this.baseUrl + 'artworks/add-image', image);
  }
  deleteArtworkImage(image: ArtworkImage) {
    return this.http.delete(this.baseUrl + 'artworks/delete-image', {
      body: image
    });
  }
  deleteArtwork(artworkId: number) {
    return this.http.delete(this.baseUrl + 'artworks/' + artworkId, {});
  }
  deleteImageDuringPostArtwork(artworkImage: ArtworkImage) {
    return this.http.delete(this.baseUrl + 'image', {
      body: artworkImage
    });
  }
}
