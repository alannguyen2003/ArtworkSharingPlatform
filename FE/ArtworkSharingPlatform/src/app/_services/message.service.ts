import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {BehaviorSubject, take} from "rxjs";
import {Message} from "../_model/message.model";
import {HttpClient} from "@angular/common/http";
import {BusyService} from "./busy.service";
import {User} from "../_model/user.model";
import {getPaginatedResult, getPaginationHeaders} from "./pagination-helper.service";

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  private messageHubConnection: HubConnection | undefined;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();
  constructor(private http: HttpClient, private busyService: BusyService) { }

  createHubConnection(user : User, otherUsername: string, artworkId: number) {
    this.busyService.busy();
    this.messageHubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'message?user=' + otherUsername + '&artworkId=' + artworkId, {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build();

    this.messageHubConnection.start()
      .catch(error => console.log(error))
      .finally(() => {
        console.log('connect to hub success');
        this.busyService.idle();
      });

    this.messageHubConnection.on('ReceiveMessageThread', messages => {
      this.messageThreadSource.next(messages);
    });

    this.messageHubConnection.on('NewMessage', message => {
      this.messageThread$.pipe(take(1)).subscribe({
        next: messages => {
          this.messageThreadSource.next([...messages, message]);
          console.log([...messages, message]);
        }
      })
    })

  }

  stopHubConnection() {
    if (this.messageHubConnection) {
      this.messageThreadSource.next([]);
      this.messageHubConnection.stop().then(() => {
        console.log("stop message hub");
      });
    }
  }

  getMessages(pageNumber: number, pageSize: number, container: string) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('container', container);
    return getPaginatedResult<Message[]>(this.baseUrl + 'messages', params, this.http);
  }

  async sendMessage(email: string, content: string, artworkId: number) {
    return this.messageHubConnection?.invoke('SendMessage', {recipientEmail: email, content, artworkId: artworkId})
      .catch(error => console.log(error));
  }

  getMessageBoxForArtist() {
    return this.http.get<Message[]>(this.baseUrl + 'messages/artistMessages');
  }

}
