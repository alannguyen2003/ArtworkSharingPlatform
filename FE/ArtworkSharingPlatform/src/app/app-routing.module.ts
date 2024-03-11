import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./components/home/home.component";
import {PricingComponent} from "./components/artist/pricing/pricing.component";
import {ArtworkComponent} from "./components/artwork/artwork.component";
import {ContactComponent} from "./components/contact/contact.component";
import {LoginComponent} from "./components/login/login.component";
import {NotfoundComponent} from "./components/error/notfound/notfound.component";
import {SignupComponent} from "./components/signup/signup.component";
import {TestErrorComponent} from "./components/error/test-error/test-error.component";
import {ServerErrorComponent} from "./components/error/server-error/server-error.component";
import {ArtworkDetailComponent} from "./components/artwork/artwork-detail/artwork-detail.component";
import {artworkDetailResolver} from "./_resolvers/artwork-detail.resolver";
import {ArtistMessageComponent} from "./components/artist/artist-message/artist-message.component";
import {ProfileEditComponent} from "./components/profile-edit/profile-edit.component";
import {ArtworkPostComponent} from "./components/artwork/artwork-post/artwork-post.component";
import {userDetailResolver} from "./_resolvers/user-detail.resolver";
import {UserProfileComponent} from "./components/user/user-profile/user-profile.component";

const routes: Routes = [
  {path: '', component: HomeComponent},
  //Phần này dành cho những route cần Guard
  {path: '',
    children: [
      {path: 'artist/messages', component: ArtistMessageComponent},
      {path: 'artist/pricing', component: PricingComponent},
      {path: 'user/user-profile/:email', component: UserProfileComponent, resolve: {userProfile: userDetailResolver}},
      {path: 'artwork', component: ArtworkComponent},
      {path: 'artwork-post', component: ArtworkPostComponent},
      {path: 'artwork/:id', component: ArtworkDetailComponent, resolve: {artwork: artworkDetailResolver}},
      {path: 'profile-edit', component: ProfileEditComponent}
    ]
  },
  {path: 'contact', component: ContactComponent},
  {path: 'login', component: LoginComponent},
  {path: 'signup', component: SignupComponent},
  {path: 'test-error', component: TestErrorComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: NotfoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
