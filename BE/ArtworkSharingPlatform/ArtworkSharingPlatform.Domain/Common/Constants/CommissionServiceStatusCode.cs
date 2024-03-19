namespace ArtworkSharingPlatform.Domain.Common.Constants;

public class CommissionServiceStatusCode
{
    public static int SUCCESS = 200;
    public static int INTERNAL_SERVER_ERROR = 500;
    public static int INVALID_ACTUAL_PRICE,INVALID_RECEIVER,
        MISSING_NOT_ACCEPTED_REASON,
        INVALID_SENDER = 400;
    public static int NOT_AN_ARTIST, NOT_AN_AUDIENCE = 403;
    public static int NO_COMMISSIONS_FOUND = 404;
    public static int COMMISSION_REQUEST_NOT_FOUND = 404;

}