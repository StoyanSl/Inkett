using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Common.Mapping;

namespace Inkett.Web.Models.ViewModels.Notifications
{
    public class NotificationViewModel : IMapFrom<Notification>
    {
        public int Id { get; set; }

        public string PictureUri { get; set; }

        public string Message { get; set; }

        public string Reference { get; set; }
    }
}
