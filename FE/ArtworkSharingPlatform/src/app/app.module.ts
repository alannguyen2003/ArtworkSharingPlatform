import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from "./_modules/shared.module";
import { HomeComponent } from './components/home/home.component';
import { NavComponent } from './components/nav/nav.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { LoadingInterceptor } from "./_interceptor/loading.interceptor";
import { PricingComponent } from "./components/artist/pricing/pricing.component";
import { ArtworkComponent } from './components/artwork/artwork.component';
import { ContactComponent } from './components/contact/contact.component';
import { LoginComponent } from './components/login/login.component';
import { NotfoundComponent } from './components/error/notfound/notfound.component';
import { SignupComponent } from './components/signup/signup.component';
import { FooterComponent } from './components/footer/footer.component';
import { HasRoleDirective } from './_directives/has-role.directive';
import { JwtInterceptor } from "./_interceptor/jwt.interceptor";
import { ServerErrorComponent } from './components/error/server-error/server-error.component';
import { TestErrorComponent } from './components/error/test-error/test-error.component';
import { ErrorInterceptor } from "./_interceptor/error.interceptor";
import { ArtworkCardComponent } from './components/artwork/artwork-card/artwork-card.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { TruncatePipe } from './_pipe/truncate.pipe';
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { MessagesComponent } from './components/messages/messages.component';
import { MessageReceivedComponent } from "./components/messages/message-received/message-received.component";
import { MessageSentComponent } from "./components/messages/message-sent/message-sent.component";
import { ProfileEditComponent } from './components/profile-edit/profile-edit.component';
import { ArtworkPostComponent } from "./components/artwork/artwork-post/artwork-post.component";
import { PhotoEditorComponent } from './components/artwork/artwork-post/photo-editor/photo-editor.component';
import { UserProfileComponent } from './components/user/user-profile/user-profile.component';
import { ArtistGalleryComponent } from './components/artist/artist-gallery/artist-gallery.component';
import { ArtworkEditComponent } from './components/artist/artist-gallery/artwork-edit/artwork-edit.component';
import { ArtworkMessageComponent } from "./components/artwork/artwork-message/artwork-message.component";
import { GalleryComponent } from "ng-gallery";
import { ReportModalComponent } from './components/modal/report-modal/report-modal.component';
import 'froala-editor/js/plugins.pkgd.min.js';
import { UserListComponent } from './components/admin/user-management/user-list/user-list.component';
import { UserDetailComponent } from './components/admin/user-management/user-detail/user-detail.component';
import { UserCreateComponent } from './components/admin/user-management/user-create/user-create.component';
import { ArtworkListComponent } from './components/admin/artwork-management/artwork-list/artwork-list.component';
import { UserUpdateComponent } from './components/admin/user-management/user-update/user-update.component';
import { OrderConfirmationComponent } from './components/checkout/order-confirmation/order-confirmation.component';
import { OrderConfirmationFailedComponent } from './components/checkout/order-confirmation-failed/order-confirmation-failed.component';
import { ChangeAvatarComponent } from './components/profile-edit/change-avatar/change-avatar.component';
import { CommonModule } from '@angular/common';
import { ArtworkCarouselComponent } from './components/home/artwork-carousel/artwork-carousel.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { AdminLayoutComponent } from './components/admin/admin-layout/admin-layout.component';
import { AdminSidebarComponent } from './components/admin/admin-sidebar/admin-sidebar.component';
import { AdminDashboardComponent } from './components/admin/admin-dashboard/admin-dashboard.component';
import { ConfirmDialogComponent } from './components/modal/confirm-dialog/confirm-dialog.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavComponent,
    PricingComponent,
    ArtworkComponent,
    ContactComponent,
    LoginComponent,
    NotfoundComponent,
    SignupComponent,
    FooterComponent,
    HasRoleDirective,
    ServerErrorComponent,
    TestErrorComponent,
    ArtworkCardComponent,
    TruncatePipe,
    TextInputComponent,
    MessagesComponent,
    ProfileEditComponent,
    ArtworkPostComponent,
    PhotoEditorComponent,
    UserProfileComponent,
    ArtistGalleryComponent,
    ArtworkEditComponent,
    ReportModalComponent,
    UserListComponent,
    UserDetailComponent,
    UserCreateComponent,
    ArtworkListComponent,
    UserUpdateComponent,

    OrderConfirmationComponent,
    OrderConfirmationFailedComponent,
    ChangeAvatarComponent,
    ArtworkCarouselComponent,
    ForgotPasswordComponent,
    AdminLayoutComponent,
    AdminSidebarComponent,
    AdminDashboardComponent,
    ConfirmDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    MessageReceivedComponent,
    MessageSentComponent,
    ArtworkMessageComponent,
    GalleryComponent,
    CommonModule,
    NgbModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  exports: [
    TruncatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
