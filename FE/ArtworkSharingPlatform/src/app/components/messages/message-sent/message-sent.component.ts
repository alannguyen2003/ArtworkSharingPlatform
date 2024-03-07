import {Component, Input} from '@angular/core';
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-message-sent',
  standalone: true,
  templateUrl: './message-sent.component.html',
  styleUrls: ['./message-sent.component.css'],
  imports:[CommonModule]
})
export class MessageSentComponent {
  @Input() content: string | undefined;
  @Input() isRead: boolean = false;
}
