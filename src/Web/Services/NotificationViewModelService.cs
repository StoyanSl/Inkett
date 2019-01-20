using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Models.ViewModels.Notifications;

namespace Inkett.Web.Services
{
    public class NotificationViewModelService : INotificationViewModelService
    {
        public List<NotificationViewModel> GetNotificationsViewModels(IReadOnlyCollection<Notification> notifications)
        {
            return Mapper.Map<IReadOnlyCollection<Notification>,List<NotificationViewModel>>(notifications);
        }
    }
}
