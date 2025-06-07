namespace Retail.Api.Core.Common;

public static class ErrorCodes
{
    // 1XXX Validation Codes
    public static readonly int INVALID_ENTITY = 1001;
    public static readonly int NOT_FOUND = 1002;

    // 2XXX Process Codes
    public static readonly int FAILED_TO_SAVE = 2001;
}
