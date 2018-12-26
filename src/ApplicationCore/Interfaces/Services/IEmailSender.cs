using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
