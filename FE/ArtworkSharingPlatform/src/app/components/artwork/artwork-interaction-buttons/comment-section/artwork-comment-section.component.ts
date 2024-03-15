import { Component,OnInit,Input,Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Artwork } from 'src/app/_model/artwork.model';
import { SharedModule } from 'src/app/_modules/shared.module';
import { CommentService } from 'src/app/_services/_comment.service';
import { ArtworkNewComment } from 'src/app/_model/artworkNewComment.model';
import {FormsModule} from '@angular/forms'
import { ArtworkComment } from 'src/app/_model/artworkComment.model';
@Component({
    selector: 'app-artwork-comment-section',
    templateUrl: './artwork-comment-section.component.html',
    styleUrls: ['./artwork-comment-section.component.css'],
    standalone:true,
    imports:[CommonModule,SharedModule,FormsModule]
})
export class ArtworkCommentSectionComponent implements OnInit {
    @Input() artwork: Artwork|undefined
    commentInput:string = ""
    commentsData: ArtworkComment[] = [];
    

    constructor(private commentService:CommentService){        
    }
    ngOnInit(): void {
        this.getCommentsFromDatabase()
    }
    async getCommentsFromDatabase(){
        console.log("Trying to retrieve the latest comments for this artwork...")
        this.commentService.getComments(this.artwork?.id as number)
        .subscribe((response)=>{ 
                                
            let tempArr = response as ArtworkComment[]
            for(let i = 0; i < tempArr.length; i++) {
                let jsonComment = tempArr[i];                                
                let eachComment = {
                    userId:jsonComment["userId"],
                    artworkId:jsonComment["artworkId"],
                    content:jsonComment["content"]
                }
                this.commentsData.push(eachComment)
            }
        }            
            )
        }
    
    
    addComment(){            
        console.log('The create comment request is sent!')
        this.commentService.addComment(this.createNewComment())
        .subscribe({
            next: (data:any) => { console.log(data) }, // completeHandler
            error: (error:any) => { console.log(error)  },    // errorHandler                         
        });
              
    }   


    createNewComment():ArtworkNewComment{
        return {            
            content:this.commentInput,
            artworkId:this.artwork?.id as number
        }
    }

}