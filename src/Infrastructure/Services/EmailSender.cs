using System;
using System.Threading.Tasks;
using Inkett.ApplicationCore.Interfaces.Services;

namespace Inkett.Infrastructure.Services
{
    class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
