import { Injectable } from '@angular/core';
import {BehaviorSubject, map} from "rxjs";
import {User} from "../_model/user.model";
import {HttpClient,HttpHeaders} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {PresenceService} from "./presence.service";
import { ArtworkNewComment } from '../_model/artworkNewComment.model';
import { ArtworkComment } from '../_model/artworkComment.model';


@Injectable({
  providedIn: 'root'
})
export class CommentService {
  baseUrl = environment.apiUrl;
  httpOptions = {
    headers: new HttpHeaders({'Content-Type': 'application/json'})
  }
//   private currentUserSource = new BehaviorSubject<User | null>(null);
//   currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient) { }

  addComment(request:ArtworkNewComment) {
    let endpoint = 'Artworks/comment?artworkId='+request.artworkId
    let commentContentJson = JSON.stringify(request.content)
    return this.http.post(this.baseUrl + endpoint,commentContentJson
            ,this.httpOptions)
    .pipe(map((response:any)=>{response.json()}))
  }
  getComments(artworkId:number){
    let endpoint = 'Artworks/comment/'+artworkId
    return this.http.get(this.baseUrl + endpoint,this.httpOptions);        
  }
}
  