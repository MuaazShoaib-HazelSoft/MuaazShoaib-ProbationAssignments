namespace UserManagementSystem.Services.AuthenticationService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
    }
}
