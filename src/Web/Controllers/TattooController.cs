using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Tattoo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web;

namespace Inkett.Web.Controllers
{
    public class TattooController : Controller
    {
        private readonly InkettUserManager _userManager;
        private readonly ITattooViewModelService _tattooViewModelService;
        private readonly IProfileViewModelService _profileViewModelService;
        private readonly IProfileService _profileService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITattooService _tattooService;
        private readonly ICommentService _commentService;

        public TattooController(ITattooViewModelService tattooViewModelService,
           InkettUserManager userManager,
           IAuthorizationService authorizationService,
            ITattooService tattooService,
            IProfileViewModelService profileViewModelService,
            ICommentService commentService,
            IProfileService profileService)
        {
            _tattooViewModelService = tattooViewModelService;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _tattooService = tattooService;
            _profileViewModelService = profileViewModelService;
            _commentService = commentService;
            _profileService = profileService;
    }
        public async Task<IActionResult> Create()
        {
            var profileId =  _userManager.GetProfileId(User);
            var viewModel = await _tattooViewModelService.GetCreateTattooViewModel(profileId);
            return this.View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CommentPost([FromBody]CommentBindingModel tattooComment)
        {
            var profileId = _userManager.GetProfileId(User);
           await _commentService.CreateComment(profileId, tattooComment.TattooId, tattooComment.CommentText);
            var profile = await _profileService.GetProfileById(profileId);
            var profileViewModel = _profileViewModelService.GetProfileViewModel(profile);
            var commentViewModel = _tattooViewModelService.GetCommentViewModel(profileViewModel, tattooComment.CommentText);
            var jsonCommentViewModel = JsonConvert.SerializeObject(commentViewModel);
            return Ok(jsonCommentViewModel);
        }
        [Route("LikeTattoo")]
        [HttpPost]
        public async Task<IActionResult> LikeTattoo([FromBody]LikeBindingModel likeModel)
        {
            var profileId = _userManager.GetProfileId(User);
            await _tattooService.CreateLike(profileId,likeModel.TattooId);
            return Ok();
        }
        [Route("DislikeTattoo")]
        [HttpPost]
        public async Task<IActionResult> DislikeTattoo([FromBody]LikeBindingModel likeModel)
        {
            var profileId = _userManager.GetProfileId(User);
            await _tattooService.RemoveLike(profileId, likeModel.TattooId);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TattooViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var profileId = _userManager.GetProfileId(User);
                await _tattooViewModelService.CreateTattooByViewModel(viewModel, profileId);
            }
           
            return RedirectToAction("Index","Profile");
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var profileId = _userManager.GetProfileId(User);
            var tattoo = await _tattooService.GetTattooWithStyles(id);
            var authorizeResultTask =  _authorizationService.AuthorizeAsync(User, tattoo, "EditPolicy");
            var viewModelTask = _tattooViewModelService.GetEditTattooViewModel(tattoo);
            if (!authorizeResultTask.GetAwaiter().GetResult().Succeeded)
            {
                return Unauthorized();
            }
            return View(viewModelTask.GetAwaiter().GetResult());
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var tattoo = await _tattooService.GetTattooWithStyles(id);
            var profileId =  _userManager.GetProfileId(User);
            if (tattoo==null)
            {
                return NotFound();
            }
            var viewModel = await _tattooViewModelService.GetIndexTattooViewModel(tattoo, profileId);
            viewModel.Profile = _profileViewModelService.GetProfileViewModel(tattoo.Profile);
            return View(viewModel);
        }

    }
}
