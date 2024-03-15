import { Component,OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
@Component({
    selector: 'app-artwork-like-button',
    templateUrl: './artwork-like-button.component.html',
    styleUrls: ['./artwork-like-button.component.css'],
    standalone:true,
    imports:[CommonModule]
})

export class ArtworkLikeButtonComponent implements OnInit {
   
    toggle = false;
    
  
    constructor() { }
  
    ngOnInit(): void {
    }
  
    toggleLike() {
        this.toggle = !this.toggle;
    }
  }