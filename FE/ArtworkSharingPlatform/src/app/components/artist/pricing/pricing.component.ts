import { Component } from '@angular/core';
import {PackageService} from "../../../_services/package.service";
import {Package} from "../../../_model/package.model";
import {environment} from "../../../../environments/environment";
import {loadStripe, Stripe} from "@stripe/stripe-js";
import {HttpClient} from "@angular/common/http";
import {PaymentService} from "../../../_services/payment.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-pricing',
  templateUrl: './pricing.component.html',
  styleUrls: ['./pricing.component.css']
})
export class PricingComponent {
  publishableKey = environment.stripePublishableKey;
  stripe: Stripe | null = null;
  packages : Package[] = [];
  constructor(private packageService : PackageService,
              private http: HttpClient,
              private paymentService: PaymentService,
              private router: Router) {
    this.packageService.loadPackages().subscribe(response => {
      this.packages = response;
    });
  }
  ngOnInit() {
    //Chỗ này để Publishable key
    loadStripe(this.publishableKey)
      .then(stripe => {
        this.stripe = stripe;
      });
  }

  pay(packageId: number) {
    this.paymentService.loadSessionStripePayment(packageId).subscribe({
      next: response => {
        this.redirectToCheckout(response.sessionId);
      },
      error: errs => {
        console.log(errs);
      }
    });
  }
  redirectToCheckout(sessionId: string){
    this.stripe?.redirectToCheckout({
      sessionId: sessionId
    });
  }
}
