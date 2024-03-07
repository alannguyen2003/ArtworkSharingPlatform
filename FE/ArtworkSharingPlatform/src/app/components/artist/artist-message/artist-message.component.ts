import {Component, OnInit, ViewChild} from '@angular/core';
import {Message} from "../../../_model/message.model";
import {MessageService} from "../../../_services/message.service";
import {CommonModule} from "@angular/common";
import {FormsModule, NgForm} from "@angular/forms";
import {RouterLink} from "@angular/router";
import {MessageSentComponent} from "../../messages/message-sent/message-sent.component";
import {MessageReceivedComponent} from "../../messages/message-received/message-received.component";
import {Artwork} from "../../../_model/artwork.model";
import {AccountService} from "../../../_services/account.service";
import {take} from "rxjs";
import {User} from "../../../_model/user.model";

@Component({
  selector: 'app-artist-message',
  standalone: true,
  templateUrl: './artist-message.component.html',
  styleUrls: ['./artist-message.component.css'],
  imports: [CommonModule, FormsModule, RouterLink, MessageSentComponent, MessageReceivedComponent]
})
export class ArtistMessageComponent implements OnInit{
  messages: Message[] = [];
  @ViewChild('messageForm') messageForm: NgForm | undefined;
  messageContent = '';
  loading = false;
  user: User | undefined;
  already = false;
  artwork: Artwork | undefined;
  audienceEmail: string = '';
  constructor(public messageService: MessageService,
              private accountServce: AccountService) {
    this.accountServce.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user
      }
    });
  }

  ngOnInit() {
    this.loadMessageBoxes();
  }

  loadMessageBoxes() {
    this.messageService.getMessageBoxForArtist().subscribe({
      next: messages => this.messages = messages
    });
  }

  checkIfConnected() {
    if (this.already) {
      this.messageService.stopHubConnection();
    }
  }
  loadSpecificMessageBox(senderEmail: string, artwork: Artwork ) {
    this.checkIfConnected();
    if (this.user) {
      this.messageService.createHubConnection(this.user, senderEmail, artwork.id);
      this.artwork = artwork;
      this.audienceEmail = senderEmail;
      this.already = true;
    }
  }

  sendMessage() {
    this.loading = true;
    if (!this.user || !this.artwork) return;
    this.messageService.sendMessage(this.audienceEmail, this.messageContent, this.artwork.id).then(() => {
      this.messageForm?.reset();
    }).finally(() => {
      this.loading = false;
    });
  }
}
