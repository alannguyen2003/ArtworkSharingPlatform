import {Artwork} from "./artwork.model";

export interface Message {
  senderId: number;
  senderEmail: string;
  senderPhotoUrl: string;
  recipientId: number;
  recipientEmail: string;
  recipientPhotoUrl: string;
  content: string;
  dateRead?: Date;
  messageSent: Date;
  artwork: Artwork;
}
