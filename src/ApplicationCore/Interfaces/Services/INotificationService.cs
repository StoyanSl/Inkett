using Inkett.ApplicationCore.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface INotificationService
    {
        Task CreateNotifications(int senderId, string PictureUri, int tattooId);
        Task<IReadOnlyCollection<Notification>> GetNotCheckedNotifications(int profileUserId);
        Task<IReadOnlyCollection<Notification>> GetCheckedNotifications(int profileUserId);
        Task CheckNotification( int notificationId);
        Task<bool> GetNotificationStatus(int profileUserId);
    }
}
