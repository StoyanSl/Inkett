using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Models.InputModels.Tattoos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Inkett.Web.Controllers.Api
{
    public class TattooController : BaseApiController
    {
        private readonly InkettUserManager _userManager;
        private readonly ITattooService _tattooService;

        public TattooController(
           InkettUserManager userManager,
           ITattooService tattooService)
        {
            _userManager = userManager;
            _tattooService = tattooService;
        }

        [HttpPost]
        public async Task LikeTattoo([FromBody]LikeInputModel likeModel)
        {
            var profileId = _userManager.GetProfileId(User);
            await _tattooService.CreateLike(profileId, likeModel.TattooId);
        }

        [HttpPost]
        public async Task DislikeTattoo([FromBody]LikeInputModel likeModel)
        {
            var profileId = _userManager.GetProfileId(User);
            await _tattooService.RemoveLike(profileId, likeModel.TattooId);
        }
    }
}

