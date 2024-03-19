import {Component, OnInit} from '@angular/core';
import {ConfigService} from "../../_services/config.service";
import {Config} from "../../_model/config.model";
import {take} from "rxjs";

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit{
  config: Config | undefined;
  constructor(private configService: ConfigService) {
  }
  ngOnInit() {
    if (!this.configService.config) {
      this.configService.getConfig().subscribe({
        next: config => {
          this.config = config;
        }
      });
    }
    else{
      this.config = this.configService.config;
    }
  }
}
