import {CommissionHistoryAdmin} from "./commissionHistoryAdmin.model";

export interface CommissionHistoryAudience {
    result: string,
    statusCode: number,
    message: string,
    returnData: CommissionHistoryAdmin[]
}
