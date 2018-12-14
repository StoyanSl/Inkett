using Microsoft.AspNetCore.Identity;

namespace Inkett.Infrastructure.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public override string UserName { get => base.UserName; set => base.UserName = value; }
    }
}
