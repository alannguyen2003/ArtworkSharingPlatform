import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FileUploader} from "ng2-file-upload";
import {environment} from "src/environments/environment";
import {Artwork} from "../../../../_model/artwork.model";
import {ArtworkService} from "../../../../_services/artwork.service";
import {AccountService} from "../../../../_services/account.service";
import {take} from "rxjs";
import {User} from "../../../../_model/user.model";
import {ArtworkImage} from "../../../../_model/artworkImage.model";

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit{
  @Input() canUploadMultiple = false;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  artwork: Artwork | undefined;
  user: User | undefined;
  images: ArtworkImage[] = [];
  @Output() artworkImagesAdded = new EventEmitter<ArtworkImage>();


  constructor(private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if(user) this.user = user;
      }
    })
  }

  ngOnInit() {
    this.initializeFileUploader();
  }

  fileOverBase(event : any) {
    this.hasBaseDropZoneOver = event;
  }

  // setMainPhoto(photo: Photo) {
  //   this.memberService.setMainPhoto(photo.id).subscribe({
  //     next: _ => {
  //       if (this.user && this.member) {
  //         this.user.photoUrl = photo.url;
  //         this.accountService.setCurrentUser(this.user);
  //         this.member.photoUrl = photo.url;
  //         this.member.photos.forEach(p => {
  //           if (p.isMain) p.isMain = false;
  //           if (p.id == photo.id) p.isMain = true;
  //         })
  //       }
  //     }
  //   });
  // }

  // deletePhoto(photoId: number) {
  //   this.memberService.deletePhoto(photoId).subscribe({
  //     next: _ => {
  //       if (this.member) {
  //         this.member.photos = this.member.photos.filter(x => x.id != photoId);
  //       }
  //     }
  //   })
  // }

  initializeFileUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + "image",
      authToken: 'Bearer ' + this.user?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    }
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo = JSON.parse(response);
        var artworkImage = {
          isThumbnail : false,
          imageUrl : photo.link,
          publicId: photo.publicId
        } as ArtworkImage;
        this.artworkImagesAdded.emit(artworkImage);
      }
    }
  }
}
