import {Component, Input, ViewChild} from '@angular/core';
import {Artwork} from "../../../_model/artwork.model";
import {User} from "../../../_model/user.model";
import {RouterLink} from "@angular/router";
import {MessageSentComponent} from "../../messages/message-sent/message-sent.component";
import {MessageReceivedComponent} from "../../messages/message-received/message-received.component";
import {CommonModule} from "@angular/common";
import {MessageService} from "../../../_services/message.service";
import {FormsModule, NgForm} from "@angular/forms";

@Component({
  selector: 'app-artwork-message',
  standalone: true,
  templateUrl: './artwork-message.component.html',
  styleUrls: ['./artwork-message.component.css'],
  imports: [CommonModule, FormsModule, RouterLink, MessageSentComponent, MessageReceivedComponent]
})
export class ArtworkMessageComponent {
  @Input() artwork: Artwork = {} as Artwork;
  @Input() user: User = {} as User;
  @ViewChild('messageForm') messageForm: NgForm | undefined;
  messageContent = '';
  loading = false;

  constructor(public messageService: MessageService) {
  }

  sendMessage() {
    this.loading = true;
    if (!this.user) return;
    this.messageService.sendMessage(this.artwork.user.email, this.messageContent, this.artwork.id).then(() => {
      this.messageForm?.reset();
    }).finally(() => {
      this.loading = false;
    });
  }

}
