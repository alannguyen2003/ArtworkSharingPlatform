import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {TabDirective, TabsetComponent} from "ngx-bootstrap/tabs";
import {Artwork} from "../../../../_model/artwork.model";
import {GalleryItem, ImageItem} from "ng-gallery";
import {User} from "../../../../_model/user.model";
import {ActivatedRoute, Router} from "@angular/router";
import {AccountService} from "../../../../_services/account.service";
import {MessageService} from "../../../../_services/message.service";
import {take} from "rxjs";
import {Genre} from "../../../../_model/genre.model";
import {ArtworkService} from "../../../../_services/artwork.service";
import {ToastrService} from "ngx-toastr";
import {ArtworkImage} from "../../../../_model/artworkImage.model";

@Component({
  selector: 'app-artwork-edit',
  templateUrl: './artwork-edit.component.html',
  styleUrls: ['./artwork-edit.component.css']
})
export class ArtworkEditComponent implements OnInit, OnDestroy{
  @ViewChild('artworkTabs', {static: true}) artworkTabs? : TabsetComponent;

  artwork: Artwork | undefined;
  images: GalleryItem[] = [];
  user: User | undefined;
  activeTab?: TabDirective;
  genres: Genre[] = [];
  validationErrors: string[] = [];

  constructor(private route: ActivatedRoute,
              private accountService: AccountService,
              private messageService: MessageService,
              private artworkService: ArtworkService,
              private toastr: ToastrService,
              private router: Router) {
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
    this.route.queryParams.subscribe({
      next: params => {
        console.log(params);
        params['tab'] && this.selectTab(params['tab'])
      }
    });
    this.loadGenres();
    console.log(this.artwork?.artworkImages);
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    console.log(this.activeTab.heading);
    console.log(this.activeTab.heading == "Messages" && this.user && this.artwork);
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


  loadGenres() {
    this.artworkService.getGenreForArtwork().subscribe({
      next: genres => {
        if (genres) this.genres = genres;
      }
    });
  }

  editArtwork() {
    if (!this.artwork) return;
    this.validationErrors = [];
    this.artworkService.updateArtworkInformation(this.artwork).subscribe({
      next: _ => {
        this.toastr.success("Update Successfully");
      },
      error: errs => this.validationErrors = errs
    })
  }

  setThumbnailImage(image: ArtworkImage) {
    this.artworkService.setThumbNail(image.id).subscribe({
      next: _ => {
        if (this.artwork) {
          this.artwork.imageUrl = image.imageUrl;
          this.artwork.artworkImages.forEach(p => {
            if (p.isThumbnail) p.isThumbnail = false;
            if (p.id == image.id) p.isThumbnail = true;
          });
          this.toastr.success('Image is set to thumbnail image');
        }
    }
    });
  }

  onArtworkImageAdd(imageData: ArtworkImage) {
    if (!this.artwork) return;
    imageData.artworkId = this.artwork.id;
    console.log(imageData.imageUrl);
    this.artworkService.addImageToArtwork(imageData).subscribe({
      next: artworkImage => {
        console.log(artworkImage);
        this.artwork?.artworkImages.push(artworkImage);
      }
    });
  }

  deleteArtworkImage(image: ArtworkImage) {
    if (!this.artwork) return;
    this.artworkService.deleteArtworkImage(image).subscribe({
      next: _ => {
        if (this.artwork) this.artwork.artworkImages = this.artwork.artworkImages.filter(x => x.id !== image.id);
        this.toastr.success("Delete image success");
      }
    });
  }

  deleteArtwork() {
    if (!this.artwork) return;
    this.artworkService.deleteArtwork(this.artwork.id).subscribe({
      next: _ => {
        this.toastr.success('Delete Successfully');
        this.router.navigateByUrl('/artist/gallery');
      }
    });
  }

  ngOnDestroy() {
    this.messageService.stopHubConnection();
  }
}
