using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.ApplicationCore.Specifications;

namespace Inkett.ApplicationCore.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IAsyncRepository<Notification> _notificationRepository;
        private readonly IProfileService _profileService;

        public NotificationService(IProfileService profileService,
            IAsyncRepository<Notification> notificationRepository)
        {
            _profileService = profileService;
            _notificationRepository = notificationRepository;
        }

        public async Task CreateNotifications(int senderId, string PictureUri, int tattooID)
        {
            var profile = await _profileService.GetProfileWithLikes(senderId);

            Guard.Against.NullOrEmpty(PictureUri, nameof(PictureUri));
            Guard.Against.Null(profile, nameof(profile));

            var notificationReference = @"/Tattoo/Index/" + tattooID.ToString();
            var message = $"{profile.Name} added new Tattoo, check it out!";

            var notifications = new List<Notification>();

            foreach (var follower in profile.Followers)
            {
                notifications.Add(new Notification(follower.ProfileId, PictureUri, notificationReference, message));
            }
            await _notificationRepository.AddRangeAsync(notifications);
        }

        public async Task<IReadOnlyCollection<Notification>> GetNotCheckedNotifications(int profileUserId)
        {
            var spec = new NotificationSpecification(profileUserId, false);
            return await _notificationRepository.ListAsync(spec);
        }
        public async Task<IReadOnlyCollection<Notification>> GetCheckedNotifications(int profileUserId)
        {
            var spec = new NotificationSpecification(profileUserId, true);
            return await _notificationRepository.ListAsync(spec);
        }

        public async Task CheckNotification(int notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);
            Guard.Against.Null(notification, nameof(notification));
            notification.IsChecked = true;
            await _notificationRepository.UpdateAsync(notification);
        }

        public async Task<bool> GetNotificationStatus(int profileUserId)
        {
            var spec = new NotificationSpecification(profileUserId, false);
            var notifications = await _notificationRepository.ListAsync(spec);
            if (notifications.Count>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
