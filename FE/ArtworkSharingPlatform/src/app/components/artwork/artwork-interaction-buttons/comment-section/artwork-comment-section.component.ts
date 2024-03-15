import { Component,OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
@Component({
    selector: 'app-artwork-comment-section',
    templateUrl: './artwork-comment-section.component.html',
    styleUrls: ['./artwork-comment-section.component.css'],
    standalone:true,
    imports:[CommonModule]
})
export class ArtworkCommentSectionComponent implements OnInit {
    cardsData: any[] = ['Alan',"Walker","Something","I Dunno What To do", "Harder better faster stronger"
    ,'Alan',"Walker","Something","I Dunno What To do", "Harder better faster stronger"];

    ngOnInit(): void {

    }
}