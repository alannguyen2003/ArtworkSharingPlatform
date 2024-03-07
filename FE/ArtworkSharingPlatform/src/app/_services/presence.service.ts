import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {BehaviorSubject, take} from "rxjs";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {User} from "../_model/user.model";
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  hubUrl = environment.hubUrl;
  private hubConnection : HubConnection | undefined;
  private onlineUserSource = new BehaviorSubject<string[]>([]);
  onlineUsers$ = this.onlineUserSource.asObservable();
  constructor(private toastr: ToastrService,
              private router: Router)
  {

  }

  createHubConnection(user: User) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'presence', {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().then(() => console.log('presence hub')).catch(error => console.log(error));
    this.hubConnection.on('UserIsOnline', email => {
      this.onlineUsers$.pipe(take(1)).subscribe({
        next: emails => {
          this.onlineUserSource.next([...emails, email]);
        }
      })
    });
    this.hubConnection.on('UserIsOffline', email => {
      this.onlineUsers$.pipe(take(1)).subscribe({
        next: emails => {
          this.onlineUserSource.next(emails.filter(x => x != email));
        }
      })
    });

    this.hubConnection.on('GetOnlineUsers', emails => {
      this.onlineUserSource.next(emails);
    });
    this.hubConnection.on('NewMessageReceived', sender => {
      if (user.roles.includes('Artist')) {
        this.toastr
          .info(sender.email + ' has sent you a new message! Click me to see the message')
          .onTap
          .pipe(take(1))
          .subscribe({
            next: () => this.router.navigateByUrl('/artist/' + sender.email + '?artworkId='+ sender.artworkId)
          });
      }
      else {
        this.toastr
          .info(sender.name + ' has sent you a new message! Click me to see the message')
          .onTap
          .pipe(take(1))
          .subscribe({
            next: () => this.router.navigateByUrl('/artwork/' + sender.artworkId + '?tab=Messages')
          });
      }
    })
  }

  stopHubConnection() {
    this.hubConnection?.stop().catch(error => console.log(error));
  }
}
