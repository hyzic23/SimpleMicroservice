namespace UserService.Constants
{
    public static class ErrorCodes
    {
        public const string SUCCESS_CODE = "00";
        public const string VALIDATION_ERROR_CODE = "01";
        public const string UNAUTHORIZE_ERROR_CODE = "401";
        public const string NOT_FOUND_ERROR_CODE = "404";

        public const string USERNAME_CANNOT_BE_EMPTY = "Username cannot be Empty.";
        public const string STAFFID_CANNOT_BE_EMPTY = "Staff Id cannot be Empty.";
        public const string USERNAME_NOT_FOUND = "Username not Found.";
        public const string INCORRECT_USERNAME_OR_STAFFID = "Incorrect Username or Staff Id";
        public const string PASSWORD_CANNOT_BE_EMPTY = "Password cannot be Empty.";
        public const string NEW_PASSWORD_CANNOT_BE_EMPTY = "New Password cannot be Empty.";
        public const string CONFIRM_PASSWORD_CANNOT_BE_EMPTY = "Confirm Password cannot be Empty.";
        public const string RESET_PASSWORD_FAILED = "Reset Password Failed.";
        public const string PASSWORD_SUCCESS = "Password Reset Successful.";
        public const string NEW_PASSWORD_CONFIRM_PASSWORD_MUST_BE_SAME = "New Password and Confirm Password must be the same.";
        public const string PASSWORD_MUST_CONTAIN_CHARACTERS = "Password must contain Special Characters.";
        public const string PASSWORD_LENGHT_MUST_BE_12_OR_GREATER_THAN = "Password Length must be 12 or greater than 12.";

        public const string PAYMENT_TYPE_CANNOT_BE_EMPTY = "Cannot create receipt as Payment method is needed.";
        public const string RECEIPT_PAYMENT_AMOUNT_CANNOT_BE_EMPTY = "Receipt Payment Amount cannot be empty or zero.";
        public const string RECEIPT_DATE_CANNOT_BE_EMPTY = "Receipt Date cannot be empty.";
        public const string PAYMENT_RECEIPT_FAILED = "Saving Payment Receipt Failed.";

        public const string SUCCESS_MESSAGE = "Successful";
    }
}
