export interface CommissionHistoryAdmin {
    id: number;
    minPrice: number;
    maxPrice: number;
    actualPrice: number;
    requestDescription?: string;
    notAcceptedReason?: string;
    requestDate: Date;
    isProgressStatus: number;
    senderName: string;
    receiverName: string;
    genreName: string;
    commissionStatus: string;
  }

  