import { Component, OnInit } from '@angular/core';
import { Transaction } from './../../../../_model/transaction.model';
import { ManagerService } from './../../../../_services/manager.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.css']
})
export class TransactionListComponent implements OnInit {
  transactions: Transaction[] = [];
  dtOptions: DataTables.Settings = {
    pagingType: 'full_numbers'
   }
   dtTrigger: Subject<any> = new Subject<any>();
  constructor(private managerService: ManagerService) {}

  ngOnInit(): void {
    this.loadAllTransactions();
  }

  loadAllTransactions() {
    this.managerService.getAllTransactions().subscribe(transactions => {
      this.transactions = transactions;
      this.dtTrigger.next(null);
    });
  }
}
