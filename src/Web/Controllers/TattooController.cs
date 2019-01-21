using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Models.InputModels.Tattoos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Inkett.Web.Controllers
{
    [Authorize]
    public class TattooController : Controller
    {
        private readonly InkettUserManager _userManager;
        private readonly ITattooViewModelService _tattooViewModelService;
        private readonly IProfileService _profileService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ITattooService _tattooService;
        private readonly ICommentService _commentService;
        private readonly INotificationService _notificationService;
       

        public TattooController(ITattooViewModelService tattooViewModelService,
           InkettUserManager userManager,
           IAuthorizationService authorizationService,
            ITattooService tattooService,
            ICommentService commentService,
            IProfileService profileService,
            INotificationService notificationService)
        {
            _tattooViewModelService = tattooViewModelService;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _tattooService = tattooService;
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
                return NotFound();
            }
            var viewModel =  _tattooViewModelService.GetTattooIndexViewModel(tattoo, profileId);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var profileId = _userManager.GetProfileId(User);
            var profile =  _profileService.GetProfileWithAlbums(profileId);
            var viewModel =  await _tattooViewModelService.GetTattooCreateViewModel(await profile);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TattooCreateInputModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var profileId = _userManager.GetProfileId(User);
                await _tattooViewModelService.CreateTattooByViewModel(viewModel, profileId);
                return RedirectToAction("Tattoos", "Profile", new { id = profileId });
            }
            return View(viewModel);
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
            var authorizeResultTask = _authorizationService.AuthorizeAsync(User, tattoo, "EditPolicy");
            if (!authorizeResultTask.GetAwaiter().GetResult().Succeeded)
            {
                return Forbid();
            }
            var viewModel = await _tattooViewModelService.GetTattooEditViewModel(tattoo);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TattooEditInputModel inputModel)
        {
           
            if (ModelState.IsValid)
            {
                var tattoo = await _tattooService.GetTattooWithStyles(id);
                var authorizeResultTask = _authorizationService.AuthorizeAsync(User, tattoo, "EditPolicy");
                if (!authorizeResultTask.GetAwaiter().GetResult().Succeeded)
                {
                    return Forbid();
                }
                var profileId = _userManager.GetProfileId(User);
                await _tattooViewModelService.EditTattooByViewModel(inputModel, tattoo);
            }
            var profile = _profileService.GetProfileWithAlbums(_userManager.GetProfileId(User));
            var viewModel = await _tattooViewModelService.GetTattooEditViewModel(inputModel,await profile);
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var tattoo = await _tattooService.GetTattooById(id);
            if (tattoo == null)
            {
                return NotFound();
            }
            var authorizeResult = await _authorizationService.AuthorizeAsync(User, tattoo, "EditPolicy");
            if (!authorizeResult.Succeeded)
            {
                return Forbid();
            }
            var viewModel = _tattooViewModelService.GetTattooListedViewModel(tattoo);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TattooDeleteInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var profileId = _userManager.GetProfileId(User);
                var tattoo = await _tattooService.GetTattooById(inputModel.Id);
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
            return RedirectToAction("Index","Profile");
        }

        [HttpPost]
        public async Task<IActionResult> CommentPost([FromBody]CommentInputModel tattooComment)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }
            var profile = await _profileService.GetProfileById(_userManager.GetProfileId(User));
            await _commentService.CreateComment(profile.Id, tattooComment.TattooId, tattooComment.CommentText);
            var commentViewModel = _tattooViewModelService.GetCommentViewModel(profile, tattooComment.CommentText);

            var jsonCommentViewModel = JsonConvert.SerializeObject(commentViewModel);
            return Ok(jsonCommentViewModel);
        }

       

    }
}
