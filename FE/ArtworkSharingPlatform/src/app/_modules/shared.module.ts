import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {NgxSpinnerModule} from "ngx-spinner";
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import {ToastrModule} from "ngx-toastr";
import {PaginationModule} from "ngx-bootstrap/pagination";
import {TabsModule} from "ngx-bootstrap/tabs";
import {TimeagoModule} from "ngx-timeago";
import {FileUploadModule} from "ng2-file-upload";
import {FroalaEditorModule, FroalaViewModule} from "angular-froala-wysiwyg";
import {ModalModule} from "ngx-bootstrap/modal";



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgxSpinnerModule.forRoot({ type: 'line-scale-party' }),
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
    TimeagoModule.forRoot(),
    FroalaViewModule.forRoot(),
    FroalaEditorModule.forRoot(),
    ModalModule.forRoot(),
    FileUploadModule
  ],
  exports: [
    NgxSpinnerModule,
    BsDropdownModule,
    ToastrModule,
    PaginationModule,
    TabsModule,
    TimeagoModule,
    FroalaViewModule,
    FroalaEditorModule,
    ModalModule,
    FileUploadModule
  ]
})
export class SharedModule { }
