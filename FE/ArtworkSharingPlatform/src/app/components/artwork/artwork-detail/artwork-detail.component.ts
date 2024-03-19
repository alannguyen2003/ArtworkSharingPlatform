import { Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, RouterLink} from "@angular/router";
import {Artwork} from "../../../_model/artwork.model";
import {CommonModule} from "@angular/common";
import {TabDirective, TabsetComponent, TabsModule} from "ngx-bootstrap/tabs";
import {GalleryItem, GalleryModule, ImageItem} from "ng-gallery";
import {ArtworkMessageComponent} from "../artwork-message/artwork-message.component";
import {AccountService} from "../../../_services/account.service";
import {take} from "rxjs";
import {User} from "../../../_model/user.model";
import {MessageService} from "../../../_services/message.service";
import { ArtworkCommentSectionComponent } from '../artwork-interaction-buttons/comment-section/artwork-comment-section.component';
import {BsModalRef, BsModalService, ModalModule} from "ngx-bootstrap/modal";
import {ReportModalComponent} from "../../modal/report-modal/report-modal.component";
import {RatingModule} from "ngx-bootstrap/rating";
import {FormsModule} from "@angular/forms";
import {ArtworkService} from "../../../_services/artwork.service";
import {Rating} from "../../../_model/rating.model";
import {ToastrService} from "ngx-toastr";
import {PresenceService} from "../../../_services/presence.service";

@Component({
  selector: 'app-artwork-detail',
  standalone: true,
  templateUrl: './artwork-detail.component.html',
  styleUrls: ['./artwork-detail.component.css'],
  imports: [CommonModule, TabsModule, FormsModule, GalleryModule, RouterLink, ArtworkMessageComponent, ModalModule, RatingModule,ArtworkCommentSectionComponent]
})
export class ArtworkDetailComponent implements OnInit, OnDestroy{
  @ViewChild('artworkTabs', {static: true}) artworkTabs? : TabsetComponent;

  artwork: Artwork | undefined;
  images: GalleryItem[] = [];
  user: User | undefined;
  activeTab?: TabDirective;
  bsModalRef: BsModalRef<ReportModalComponent> = new BsModalRef<ReportModalComponent>();
  x = 5;
  y = 0;
  phoneNumberButtonState = 0;
  constructor(private route: ActivatedRoute,
              private accountService: AccountService,
              private messageService: MessageService,
              private modalService: BsModalService,
              private artworkService: ArtworkService,
              public presenceService: PresenceService,
              private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user;
      }
    });
  }
  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.artwork = data['artwork'];
      }
    });
    console.log(this.artwork);
    this.route.queryParams.subscribe({
      next: params => {
        console.log(params);
        params['tab'] && this.selectTab(params['tab'])
      }
    });
    this.getImages();
    this.getRatingForUser();
  }

  getRatingForUser() {
    if (!this.artwork) return;
    this.artworkService.getArtworkRatingForUser(this.artwork.id).subscribe({
      next: rating => {
        this.y = rating;
      }
    });
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if (this.activeTab.heading == "Messages" && this.user && this.artwork) {
      this.messageService.createHubConnection(this.user, this.artwork.user.email, this.artwork.id);
    }
    else {
      this.messageService.stopHubConnection();
    }
  }

  selectTab(heading: string) {
    console.log(this.artworkTabs);
    if (this.artworkTabs) {
      this.artworkTabs.tabs.find(x => x.heading === heading)!.active = true;
    }

  }

  getImages() {
    if (!this.artwork) return;

    for (const image of this.artwork.artworkImages) {
      this.images.push(new ImageItem({src: image.imageUrl, thumb: image.imageUrl}));
    }
  }
  openReportModal() {
    const config = {
      class: 'modal-dialog-centered modal-lg',
      initialState: {
        artwork: this.artwork
      }
    };
    this.bsModalRef = this.modalService.show(ReportModalComponent, config);
  }

  rating(v: number) {
    if (!this.artwork) return;
    var rating: Rating = {
      artworkId : this.artwork.id,
      score : v
    }
    this.artworkService.rating(rating).subscribe({
      next: _ => {
        this.toastr.success('Rating successfully!');
      }
    });
  }

  displayPhoneNumber() {
    // Change state to loading
    this.phoneNumberButtonState= 1;

    // Emulate a delay of 1 second and then display the phone number
    setTimeout(() => {
      this.phoneNumberButtonState = 2;
    }, 1000);
  }
  hidePhoneNumber() {
    // Change state to loading
    this.phoneNumberButtonState= 1;

    // Emulate a delay of 1 second and then display the phone number
    setTimeout(() => {
      this.phoneNumberButtonState = 0;
    }, 1000);
  }

  ngOnDestroy() {
    this.messageService.stopHubConnection();
  }
}
