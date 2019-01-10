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
        
        InkettUserManager _userManager;
        public ResourceAuthorizationHandler(
            InkettUserManager userManager)
        {
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
