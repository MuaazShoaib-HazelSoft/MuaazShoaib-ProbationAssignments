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

        // Error Messages
        public const string UserNotFound = "User not found";
        public const string NoUsers = "No users registered at this moment.";
        public const string InvalidId = "Valid user Id must be provided.";
        public const string EmailAlreadyExists = "User with this email already exists.";
        public const string ErrorOccured = "An unexpected Error Occured.";
        public const string InvalidEmail = "Invalid Email Format";
        public const string InvalidPassword = "Password Length must be at least 5 characters Long.";
        public const string AgeRangeError = "Age must be between 18-100";


        //Required Messages
        public const string NameRequired = "Name is Required";
        public const string EmailRequired = "Email is Required";
        public const string PasswordRequired = "Password is Required";
        public const string AgeRequired = "Age is Required";


    }
}
