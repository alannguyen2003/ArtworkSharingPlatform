import { AdminService } from 'src/app/_services/admin.service';
import { Transaction } from './../../../../_model/transaction.model';
import { Component } from '@angular/core';
import { ManagerService } from 'src/app/_services/manager.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-transaction-detail',
  templateUrl: './transaction-detail.component.html',
  styleUrls: ['./transaction-detail.component.css']
})
export class TransactionDetailComponent {
  transaction: Transaction;

  constructor(
    private managerService: ManagerService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getReportDetail();
  }

  getReportDetail(): void {
    const reportId = +this.route.snapshot.paramMap.get('id')!;
    this.managerService.getTransactionDetail(reportId).subscribe((transaction) => {
      this.transaction = transaction;
    });
  }

  exportTransaction(): void {
    const transactionId = this.transaction?.id;
    if (transactionId) {
      this.managerService.exportTransaction(transactionId).subscribe((data) => {
        // Code to handle file download
        this.downloadFile(data);
      });
    }
  }
  
  downloadFile(data: Blob): void {
    const url = window.URL.createObjectURL(data);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = `Transaction-${this.transaction?.id}.xlsx`; // or .xlsx depending on your backend
    document.body.appendChild(anchor);
    anchor.click();
    document.body.removeChild(anchor);
    window.URL.revokeObjectURL(url);
  }
  
}
