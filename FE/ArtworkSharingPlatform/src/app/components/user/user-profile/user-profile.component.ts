import {Component, OnInit} from '@angular/core';
import {UserProfile} from "../../../_model/userProfile.model";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit{
  userProfile: UserProfile | undefined;

  constructor(private route: ActivatedRoute) {
  }
  ngOnInit() {
    this.route.data.subscribe({
      next: data => {
        this.userProfile = data['userProfile'];
      }
    });
  }
}
