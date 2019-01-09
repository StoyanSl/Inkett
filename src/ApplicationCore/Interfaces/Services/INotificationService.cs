using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface INotificationService
    {
       Task CreateNotifications(int senderId, string PictureUri, int tattooId);

    }
}
