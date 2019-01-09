using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Inkett.Infrastructure.Identity
{
    public class InkettUserManager : UserManager<ApplicationUser>
    {
        private const string ProfileIdClaimType = "ProfileId";
        private const string ProfileNotificatinStatusClaimType = "NotificationStatus"; 
        public InkettUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) :
            base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

        }

        public int GetProfileId(ClaimsPrincipal principal)
        {
            var id = ((ClaimsIdentity)principal.Identity).Claims.First(c => c.Type == ProfileIdClaimType).Value;
            return int.Parse(id);
        }
        public async Task SetUserFirstNameAsync(ApplicationUser user, string firstName)
        {
            user.FirstName = firstName;
            await this.UpdateAsync(user);
        }
        public async Task SetUserLastNameAsync(ApplicationUser user, string lastName)
        {
            user.LastName = lastName;
            await this.UpdateAsync(user);
        }
        public async Task SetUserBirthDayAsync(ApplicationUser user, DateTime? birthdayDate)
        {
            user.BirthdayDate = birthdayDate;
            await this.UpdateAsync(user);
        }
    }
}
