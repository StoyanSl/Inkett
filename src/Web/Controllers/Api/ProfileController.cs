using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inkett.Web.Controllers.Api
{
    public class ProfileController : BaseApiController
    {
        private readonly InkettUserManager _userManager;
        private readonly IProfileService _profileService;

        public ProfileController(
          InkettUserManager userManager,
         IProfileService profileService)
        {
            _userManager = userManager;
            _profileService = profileService;

        }

        [HttpPost]
        public async Task<IActionResult> FollowProfile(int profileId)
        {
            var followerId = _userManager.GetProfileId(User);
            if (profileId == followerId)
            {
                return NotFound();
            }
            await _profileService.CreateFollow(followerId, profileId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UnFollowProfile(int profileId)
        {
            var followerId = _userManager.GetProfileId(User);
            if (profileId == followerId)
            {
                return NotFound();
            }
            await _profileService.RemoveFollow(followerId, profileId);
            return Ok();
        }
    }
}
