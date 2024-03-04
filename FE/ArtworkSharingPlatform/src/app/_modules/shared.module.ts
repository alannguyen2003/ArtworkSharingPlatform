import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {NgxSpinnerModule} from "ngx-spinner";
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import {ToastrModule} from "ngx-toastr";
import {PaginationModule} from "ngx-bootstrap/pagination";



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgxSpinnerModule.forRoot({ type: 'line-scale-party' }),
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right'
    }),
    PaginationModule.forRoot()
  ],
  exports: [
    NgxSpinnerModule,
    BsDropdownModule,
    ToastrModule,
    PaginationModule
  ]
})
export class SharedModule { }
