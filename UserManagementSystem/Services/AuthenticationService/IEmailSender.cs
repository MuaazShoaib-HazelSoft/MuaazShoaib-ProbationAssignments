namespace UserManagementSystem.Services.AuthenticationService
{
    public interface IEmailSender
    {
        /// <summary>
        /// Send Email Method to
        /// be implemented.
        /// </summary>
        Task  SendEmailAsync(string toEmail, string subject, string htmlMessage);
    }
}
