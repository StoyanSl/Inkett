using Inkett.ApplicationCore.Entitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inkett.ApplicationCore.Specifications
{
    public sealed class NotificationSpecification : BaseSpecification<Notification>
    {
        public NotificationSpecification(int profileUserId, bool isChecked)
            : base(n => n.ProfileId == profileUserId && n.IsChecked== isChecked)
        {

        }
    }
}
