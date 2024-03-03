import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {SharedModule} from "./_modules/shared.module";
import { HomeComponent } from './components/home/home.component';
import { NavComponent } from './components/nav/nav.component';
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import {LoadingInterceptor} from "./_interceptor/loading.interceptor";
import {PricingComponent} from "./components/artist/pricing/pricing.component";
import { ArtworkComponent } from './components/artwork/artwork.component';
import { ContactComponent } from './components/contact/contact.component';
import { LoginComponent } from './components/login/login.component';
import { NotfoundComponent } from './components/error/notfound/notfound.component';
import { SignupComponent } from './components/signup/signup.component';
import { FooterComponent } from './components/footer/footer.component';

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
    FooterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS,
    useClass: LoadingInterceptor,
    multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
