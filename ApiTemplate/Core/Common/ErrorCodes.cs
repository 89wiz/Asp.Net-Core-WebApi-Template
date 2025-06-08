namespace ApiTemplate.Core.Common;

public static class ErrorCodes
{
    // 1XXX Validation Codes
    public const int INVALID_ENTITY = 1001;
    public const int NOT_FOUND = 1002;

    // 2XXX Process Codes
    public const int FAILED_TO_SAVE = 2001;

    // 3XXX Auth
    public const int EMAIL_ALREADY_REGISTERED = 3001;
    public const int USERNAME_ALREADY_REGISTERED = 3002;
}
