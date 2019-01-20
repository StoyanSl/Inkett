using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inkett.Web.Controllers
{
    [Authorize]
    public class NotificationController:Controller
    {
        private readonly InkettUserManager _inkettUserManager;
        private readonly INotificationService _notificationService;
        private readonly INotificationViewModelService _notificationViewModelService;


        public NotificationController(INotificationViewModelService notificationViewModelService,
            InkettUserManager inkettUserManager,
            INotificationService notificationService)
        {
            _notificationViewModelService = notificationViewModelService;
            _inkettUserManager = inkettUserManager;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userProfileId = _inkettUserManager.GetProfileId(User);
            var notCheckedNotifications = await _notificationService.GetNotCheckedNotifications(userProfileId);

            var viewModels = _notificationViewModelService.GetNotificationsViewModels(notCheckedNotifications);
            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Checked()
        {
            var userProfileId = _inkettUserManager.GetProfileId(User);
            var checkedNotifications = await _notificationService.GetCheckedNotifications(userProfileId);

            var viewModels = _notificationViewModelService.GetNotificationsViewModels(checkedNotifications);
            return View(viewModels);
        }

        [HttpPost]
        public async Task CheckNotification(int notificationId)
        {
            var userProfileId = _inkettUserManager.GetProfileId(User);
            var userId = _inkettUserManager.GetUserId(User);
            await _notificationService.CheckNotification(notificationId);
        }
    }
}
