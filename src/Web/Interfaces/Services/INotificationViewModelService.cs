using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Models.ViewModels.Notifications;
using System.Collections.Generic;

namespace Inkett.Web.Interfaces.Services
{
    public interface INotificationViewModelService
    {
       List<NotificationViewModel> GetNotificationsViewModels(IReadOnlyCollection<Notification> notifications);
    }
}
