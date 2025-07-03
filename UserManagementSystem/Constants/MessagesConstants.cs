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
        public const string RoleAdded = "Role created successfully";
        public const string RoleUpdated = "Role updated successfully";
        public const string RoleDeleted = "Role deleted successfully";
        public const string RoleFetched = "Roles fetched successfully";
        public const string RoleAssigned = "Role assigned successfully";
        public const string CourseAdded = "Course Added successfully";
        public const string CourseUpdated = "Course Updated successfully";
        public const string CourseDeleted = "Course Deleted successfully";
        public const string CourseFetched = "Courses Fetched successfully";
        public const string CourseAssigned = "Course Assigned to User successfully";

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
        public const string FailedEmailConfirmation = "Email Confirmation Failed. Token may be invalid or expired.";
        public const string EmailConfirmationSuccess = "Your Email has been Confirmed Successfully.";
        public const string EmailNotConfirmedYet = "Please Confirm Your Email before Logging In.";
        public const string EmailFailedtoSend = "Email Failed to Send.";
        public const string InvalidPages = "Page Size or Page Number must be a valid positive number.";
        public const string RoleExists = "Role Already Exists.";
        public const string RoleNotFound = "Role Not Found.";
        public const string CourseNotFound = "Course Not Found.";
        public const string NoRoleFound = "No Role Exists.";
        public const string NoCourseFound = "No Course Exists.";
        public const string CourseDeletionFailed = "Course Failed to Delete.";
        public const string CourseUpdationFailed = "Course Failed to Update.";
        public const string CourseAddFailed = "Course Failed to Add";
        public const string CourseAlreadyExists = "Course name already exists.";
        public const string CourseEnrolled = "User is already Enrolled in this course.";
        public const string CourseAssignFailed = "Failed to Assign the Course to User.";

        //Required Messages
        public const string NameRequired = "Username is Required";
        public const string EmailRequired = "Email is Required";
        public const string PasswordRequired = "Password is Required";
        public const string AgeRequired = "Age is Required";
        public const string EmailConfirm = "Confirm your Email";
        public const string RoleNameRequired = "Role name is Required";
        public const string CourseNameRequired = "Course name is Required";
        public const string CourseIdRequired = "Course Id is Required";
        public const string UserIdRequired = "User Id is Required";
    }
}
