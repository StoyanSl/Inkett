using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Notification;

namespace Inkett.Web.Services
{
    public class NotificationViewModelService : INotificationViewModelService
    {
        public List<NotificationViewModel> GetNotificationsViewModels(IReadOnlyCollection<Notification> notifications)
        {
            var notificationsViewModels = notifications.Select(n => new NotificationViewModel()
            {
                Id = n.Id,
                Message=n.Message,
                PictureUri=n.PictureUri,
                Reference=n.Reference
            }).ToList();
            return notificationsViewModels;
        }
    }
}
