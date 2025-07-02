namespace UserManagementSystem.Services.AuthenticationService.EmailService
{
    public interface IEmailSender
    {
        /// <summary>
        /// interface containing Send Email Method 
        /// to  be implemented.
        /// </summary>
        Task  SendEmailAsync(string toEmail, string subject, string htmlMessage);
    }
}
