import { ResolveFn } from '@angular/router';
import {inject} from "@angular/core";
import {ArtworkService} from "../_services/artwork.service";

export const artworkDetailResolver: ResolveFn<boolean> = (route, state) => {
  const artworkService = inject(ArtworkService);
  return artworkService.getArtwork(+route.paramMap.get('id')!);
};
