namespace UserManagementSystem
{
    public static class MessagesConstants
    {
        // Success Messages
        public const string UserAdded = "User added successfully";
        public const string UserUpdated = "User data updated successfully";
        public const string UserDeleted = "User deleted successfully";
        public const string UsersFetched = "Users fetched successfully";
        public const string UserFetched = "User fetched successfully";
        public const string UserLogin = "User logged in successfully";
        public const string PasswordUpdated = "Password Updated Successfully.";

        // Error Messages
        public const string UserNotFound = "User not found";
        public const string NoUsers = "No users registered at this moment.";
        public const string InvalidId = "Valid user Id must be provided.";
        public const string InvalidUser = "Invalid Email or Password";
        public const string EmailAlreadyExists = "User with this email already exists.";
        public const string ErrorOccured = "An unexpected Error Occured.";
        public const string InvalidEmail = "Invalid Email Format";
        public const string InvalidPassword = "Password Length must be at least 5 characters Long.";
        public const string UnmatchedPasswords = "Passwords dont match.";
        public const string PasswordNotUpdated = "Passwords not Updated Successfully.";
        public const string InvalidHttp = "Method Not Allowed. Please check the HTTP verb used for this endpoint.";
        public const string UnauthorizedToken = "Unauthorized Request. Invalid Token.";
        public const string DeletionFailed = "Error in Deleting the User.";
        public const string UpdationFailed = "Error in Updating the User.";
        public const string InvalidContentType = "415 Unsupported Media Type: Missing Content-Type or Body";
        public const string UnmatchedCriteria = "No User Found.";
        //Required Messages
        public const string NameRequired = "Username is Required";
        public const string EmailRequired = "Email is Required";
        public const string PasswordRequired = "Password is Required";
        public const string AgeRequired = "Age is Required";


    }
}
