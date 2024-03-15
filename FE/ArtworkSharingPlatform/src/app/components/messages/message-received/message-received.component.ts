import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-message-received',
  standalone: true,
  templateUrl: './message-received.component.html',
  styleUrls: ['./message-received.component.css']
})
export class MessageReceivedComponent {
  @Input() username: string | undefined;
  @Input() content: string | undefined;
  @Input() userImageUrl: string | undefined;
}
