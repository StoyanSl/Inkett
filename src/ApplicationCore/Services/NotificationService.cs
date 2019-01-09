﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Entitites.Notifications;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;

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
            var profile =await _profileService.GetProfileWithLikes(senderId);

            Guard.Against.NullOrEmpty(PictureUri, nameof(PictureUri));
            Guard.Against.Null(profile,nameof(profile));

            var notificationReference = @"/Tattoo/Index/" + tattooID.ToString();
            var message = $"{profile.ProfileName} added new Tattoo, check it out!";

            var notifications = new List<Notification>();

            foreach (var follower in profile.Followers)
            {
                notifications.Add( new Notification(profile.Id,PictureUri,notificationReference,message));
            }
            await _notificationRepository.AddRangeAsync(notifications);
        }
    }
}