using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Viewmodels.Notification;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inkett.Web.Interfaces.Services
{
    public interface INotificationViewModelService
    {
       List<NotificationViewModel> GetNotificationsViewModels(IReadOnlyCollection<Notification> notifications);
    }
}
