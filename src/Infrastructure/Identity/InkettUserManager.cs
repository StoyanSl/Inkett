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
    public class InkettUserManager:UserManager<ApplicationUser>
    {
        private const string ProfileIdClaimType = "ProfileId";
        public InkettUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {

        }

        public int GetProfileId(ClaimsPrincipal principal)
        {
            var id = ((ClaimsIdentity)principal.Identity).Claims.First(c => c.Type == ProfileIdClaimType).Value;
            return int.Parse(id);
        }
        
    }
}
