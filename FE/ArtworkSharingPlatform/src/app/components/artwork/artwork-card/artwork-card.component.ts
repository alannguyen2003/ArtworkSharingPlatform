import {Component, Input, OnInit} from '@angular/core';
import {Artwork} from "../../../_model/artwork.model";
import {User} from "../../../_model/user.model";
import {AccountService} from "../../../_services/account.service";
import {take} from "rxjs";
import {ArtworkLike} from "../../../_model/artworkLike.model";
import {ArtworkService} from "../../../_services/artwork.service";

@Component({
  selector: 'app-artwork-card',
  templateUrl: './artwork-card.component.html',
  styleUrls: ['./artwork-card.component.css']
})
export class ArtworkCardComponent implements OnInit{
  @Input() artwork: Artwork | undefined;
  user: User | undefined;
  checkIsLiked = false;

  constructor(private accoutnService: AccountService,
              private artworkService: ArtworkService) {
    this.accoutnService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user;
      }
    });
  }

  ngOnInit() {
    this.checkLiked();
  }

  checkLiked() {
    if (!this.artwork) return;
    this.checkIsLiked = this.artwork.likes.some(x => x.userEmail == this.user?.email);
  }

  likeArtwork() {
    if (!this.user || !this.artwork) return;
    this.artworkService.likeArtwork(this.artwork.id).subscribe({
      next: _ => {
        var index = this.artwork?.likes.findIndex(x => x.userEmail == this.user?.email);
        if (index! >= 0) {
          this.artwork?.likes.splice(index!, 1);
          console.log(this.artwork?.likes);
        }
        else {
          this.artwork?.likes.push({artworkId : this.artwork?.id, userEmail : this.user?.email} as ArtworkLike);
        }
        this.checkIsLiked = !this.checkIsLiked;
      }
    });
  }

}
