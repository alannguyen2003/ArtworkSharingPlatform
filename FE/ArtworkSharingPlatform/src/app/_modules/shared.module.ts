import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {NgxSpinnerModule} from "ngx-spinner";
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import {ToastrModule} from "ngx-toastr";
import {PaginationModule} from "ngx-bootstrap/pagination";
import {TabsModule} from "ngx-bootstrap/tabs";
import {TimeagoModule} from "ngx-timeago";
import {FileUploadModule} from "ng2-file-upload";



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
    FileUploadModule
  ],
  exports: [
    NgxSpinnerModule,
    BsDropdownModule,
    ToastrModule,
    PaginationModule,
    TabsModule,
    TimeagoModule,
    FileUploadModule
  ]
})
export class SharedModule { }
