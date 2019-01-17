using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Common;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Tattoo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Inkett.Web.Controllers
{
    [Authorize]
    public class TattooController : Controller
    {
        private readonly InkettUserManager _userManager;
        private readonly ITattooViewModelService _tattooViewModelService;
        private readonly IProfileViewModelService _profileViewModelService;
        private readonly IProfileService _profileService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITattooService _tattooService;
        private readonly ICommentService _commentService;
        private readonly INotificationService _notificationService;

        public TattooController(ITattooViewModelService tattooViewModelService,
           InkettUserManager userManager,
           IAuthorizationService authorizationService,
            ITattooService tattooService,
            IProfileViewModelService profileViewModelService,
            ICommentService commentService,
            IProfileService profileService,
            INotificationService notificationService)
        {
            _tattooViewModelService = tattooViewModelService;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _tattooService = tattooService;
            _profileViewModelService = profileViewModelService;
            _commentService = commentService;
            _profileService = profileService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var tattoo = await _tattooService.GetTattooWithStyles(id);
            var profileId = _userManager.GetProfileId(User);
            if (tattoo == null)
            {
                throw new ApplicationException(ExceptionMessages.TattooNotFound);
            }
            var viewModel = await _tattooViewModelService.GetIndexTattooViewModel(tattoo, profileId);
            viewModel.Profile = _profileViewModelService.GetProfileViewModel(tattoo.Profile);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var profileId = _userManager.GetProfileId(User);
            var viewModel = await _tattooViewModelService.GetCreateTattooViewModel(profileId);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TattooViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var profileId = _userManager.GetProfileId(User);
                await _tattooViewModelService.CreateTattooByViewModel(viewModel, profileId);
                return RedirectToAction("Tattoos","Profile", new { id=profileId});
            }
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> CommentPost([FromBody]CommentBindingModel tattooComment)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }
            var profileId = _userManager.GetProfileId(User);
            var profile = await _profileService.GetProfileById(profileId);
            var profileViewModel = _profileViewModelService.GetProfileViewModel(profile);

            await _commentService.CreateComment(profileId, tattooComment.TattooId, tattooComment.CommentText);
            var commentViewModel = _tattooViewModelService.GetCommentViewModel(profileViewModel, tattooComment.CommentText);
            
            var jsonCommentViewModel = JsonConvert.SerializeObject(commentViewModel);
            return Ok(jsonCommentViewModel);
        }

        [Route("LikeTattoo")]
        [HttpPost]
        public async Task LikeTattoo([FromBody]LikeBindingModel likeModel)
        {
            var profileId = _userManager.GetProfileId(User);
            await _tattooService.CreateLike(profileId, likeModel.TattooId);
        }

        [Route("DislikeTattoo")]
        [HttpPost]
        public async Task DislikeTattoo([FromBody]LikeBindingModel likeModel)
        {
            var profileId = _userManager.GetProfileId(User);
            await _tattooService.RemoveLike(profileId, likeModel.TattooId);
        }
       
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var profileId = _userManager.GetProfileId(User);
            var tattoo = await _tattooService.GetTattooWithStyles(id);
            if (tattoo == null)
            {
                return NotFound();
            }
            var authorizeResultTask =  _authorizationService.AuthorizeAsync(User, tattoo, "EditPolicy");
            var viewModelTask = _tattooViewModelService.GetEditTattooViewModel(tattoo);
            if (!authorizeResultTask.GetAwaiter().GetResult().Succeeded)
            {
                return Forbid();
            }
            return View(viewModelTask.GetAwaiter().GetResult());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditTattooViewModel viewModel)
        {
            var tattoo = await _tattooService.GetTattooWithStyles(id);
            if (ModelState.IsValid)
            {
                var profileId = _userManager.GetProfileId(User);
                await _tattooViewModelService.EditTattooByViewModel(viewModel, tattoo);
            }
             viewModel = await _tattooViewModelService.GetEditTattooViewModel(tattoo);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var profileId = _userManager.GetProfileId(User);
            var tattoo = await _tattooService.GetTattooById(id);
            if (tattoo == null)
            {
                return NotFound();
            }
            var authorizeResultTask = _authorizationService.AuthorizeAsync(User, tattoo, "EditPolicy");
            var viewModelTask = _tattooViewModelService.GetListedTattooViewModel(tattoo);
            if (!authorizeResultTask.GetAwaiter().GetResult().Succeeded)
            {
                return Forbid();
            }
            return View(viewModelTask);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ListedTattooViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var profileId = _userManager.GetProfileId(User);
                var tattoo = await _tattooService.GetTattooById(viewModel.Id);
                if (tattoo == null)
                {
                    return NotFound();
                }
                var authorization = await _authorizationService.AuthorizeAsync(User, tattoo, "EditPolicy");
                if (!authorization.Succeeded)
                {
                    return Forbid();
                }
                await _tattooService.RemoveTattoo(tattoo);
                return RedirectToAction("Index", "Profile");
            }
            return View(viewModel);
        }
            
    }
}
