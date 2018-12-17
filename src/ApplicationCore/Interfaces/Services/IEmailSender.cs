using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
