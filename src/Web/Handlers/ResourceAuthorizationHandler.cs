using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Inkett.Web.Handlers
{
    public class ResourceAuthorizationHandler:AuthorizationHandler<SameProfileRequirement, IProfileAuthorizable>
    {
        private readonly IProfileService _profileService;
        InkettUserManager _userManager;
        public ResourceAuthorizationHandler(IProfileService profileService,
            InkettUserManager userManager)
        {
            _profileService = profileService;
            _userManager = userManager;
        }

        protected  override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SameProfileRequirement requirement,
                                                       IProfileAuthorizable resource)
        {
            var profileId =  _userManager.GetProfileId(context.User);
            if (profileId == resource.ProfileId)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

   
}
