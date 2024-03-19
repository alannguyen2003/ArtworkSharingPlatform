import { NgModule, Component } from '@angular/core';
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
import {ArtistGalleryComponent} from "./components/artist/artist-gallery/artist-gallery.component";
import {ArtworkEditComponent} from "./components/artist/artist-gallery/artwork-edit/artwork-edit.component";
import { UserListComponent } from './components/admin/user-management/user-list/user-list.component';
import { UserDetailComponent } from './components/admin/user-management/user-detail/user-detail.component';
import { UserCreateComponent } from './components/admin/user-management/user-create/user-create.component';
import { ArtworkListComponent } from './components/admin/artwork-management/artwork-list/artwork-list.component';
import { UserUpdateComponent } from './components/admin/user-management/user-update/user-update.component';
import { AdminLayoutComponent } from './components/admin/admin-layout/admin-layout.component';
import { AdminDashboardComponent } from './components/admin/admin-dashboard/admin-dashboard.component';
import { CommissionListComponent } from './components/admin/commission-management/commission-list/commission-list.component';
import { ConfigCreateComponent } from './components/admin/config-magement/config-create/config-create.component';
import { CommissionDetailComponent } from './components/admin/commission-management/commission-detail/commission-detail.component';
import { ReportDetailComponent } from './components/admin/report-management/report-detail/report-detail.component';
import { ReportListComponent } from './components/admin/report-management/report-list/report-list.component';
import { ReportUpdateComponent } from './components/admin/report-management/report-update/report-update.component';
import {OrderConfirmationComponent} from "./components/checkout/order-confirmation/order-confirmation.component";
import {
  OrderConfirmationFailedComponent
} from "./components/checkout/order-confirmation-failed/order-confirmation-failed.component";
import {ForgotPasswordComponent} from "./components/forgot-password/forgot-password.component";
import {authGuard} from "./_guards/auth.guard";
import {adminGuard} from "./_guards/admin.guard";
import {preventUnsavedChangesUserGuard} from "./_guards/prevent-unsaved-changes-user.guard";
import {CommissionsComponent} from "./components/user/commissions/commissions.component";
import {DetailComponent} from "./components/user/commissions/detail/detail.component";
import {CommissionArtistComponent} from "./components/user/commission-artist/commission-artist.component";

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: 'admin',
    // runGuardsAndResolvers: 'always',
    // canActivate: [adminGuard],
    component: AdminLayoutComponent,
    children: [
      { path: 'dashboard', component: AdminDashboardComponent },
      { path: 'user-management/user-list', component: UserListComponent },
      { path: 'user-detail/:email', component: UserDetailComponent },
      { path: 'user-management/user-create', component: UserCreateComponent },
      { path: 'user-update/:email', component: UserUpdateComponent },
      { path: 'artwork-management/artwork-list', component: ArtworkListComponent },
      { path: 'config-management/config-create', component: ConfigCreateComponent },
      { path: 'commission-management/commission-list', component: CommissionListComponent },
      { path: 'commission-detail/:commissionId', component: CommissionDetailComponent},
      { path: 'report-management/report-list', component: ReportListComponent },
    ],
  },

  //Phần này dành cho những route cần Guard
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'artist/messages', component: ArtistMessageComponent },
      { path: 'artist/pricing', component: PricingComponent },
      { path: 'artist/gallery', component: ArtistGalleryComponent },
      {
        path: 'user/user-profile/:email',
        component: UserProfileComponent,
        resolve: { userProfile: userDetailResolver },
      },
      { path: 'artwork', component: ArtworkComponent },
      { path: 'artwork-post', component: ArtworkPostComponent },
      {
        path: 'artwork/:id',
        component: ArtworkDetailComponent,
        resolve: { artwork: artworkDetailResolver },
      },
      {
        path: 'artwork-edit/:id',
        component: ArtworkEditComponent,
        resolve: { artwork: artworkDetailResolver },
      },
      { path: 'profile-edit',
        component: ProfileEditComponent,
        canDeactivate: [preventUnsavedChangesUserGuard]
      },
    ],
  },
  {path: 'contact', component: ContactComponent},
  {path: 'checkout', component: OrderConfirmationComponent},
  {path: 'checkout-fail', component: OrderConfirmationFailedComponent},
  {path: 'login', component: LoginComponent},
  {path: 'pre-orders', component: CommissionsComponent},
  {path: 'pre-orders/:id', component: DetailComponent},
  {path: 'commissions', component: CommissionArtistComponent},
  {path: 'forgot-password', component: ForgotPasswordComponent},
  {path: 'signup', component: SignupComponent},
  {path: 'test-error', component: TestErrorComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: NotfoundComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
