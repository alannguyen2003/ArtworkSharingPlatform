import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  getDataFromLocal(param: string) {
    let data: any = localStorage.getItem(param);
    return localStorage.getItem(param)? JSON.parse(data) : null;
  }

}
