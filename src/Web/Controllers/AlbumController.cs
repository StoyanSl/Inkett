using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Album;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inkett.Web.Controllers
{
    public class AlbumController : Controller
    {
        private readonly InkettUserManager _userManager;
        private readonly IAlbumService _albumService;
        private readonly IProfileService _profileService;
        private readonly IProfileViewModelService _profileViewModelService;
        private readonly IAlbumViewModelService _albumViewModelService;
        private readonly IAuthorizationService _authorizationService;

        public AlbumController(
            InkettUserManager userManager,
            IAlbumService albumService,
            IProfileService profileService,
            IAlbumViewModelService albumViewModelService,
            IAuthorizationService authorizationService,
            IProfileViewModelService profileViewModelService)
        {
            _albumService = albumService;
            _profileService = profileService;
            _userManager = userManager;
            _albumViewModelService = albumViewModelService;
            _authorizationService = authorizationService;
            _profileViewModelService = profileViewModelService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var album = await _albumService.GetAlbumWithTattoos(id);
            var profile = await _profileService.GetProfileById(album.ProfileId);
            if (album == null)
            {
                return NotFound();
            }
            var authorization = _authorizationService.AuthorizeAsync(User, album, "EditPolicy");
            var viewModel = _albumViewModelService.GetIndexAlbumViewModel(album);
            var authResult = await authorization;
            if (authResult.Succeeded)
            {
                viewModel.isOwner = true;
            }
            viewModel.Profile =  _profileViewModelService.GetProfileViewModel(profile);
            return View(viewModel);

        }
        public IActionResult Create()
        {
            return this.View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AlbumViewModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var profileId = _userManager.GetProfileId(User);
                await _albumService.CreateAlbum(profileId, bindingModel.Title, bindingModel.Description, bindingModel.Picture);
            }
            return View(bindingModel);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var album = await _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            var authorization = await _authorizationService.AuthorizeAsync(User, album, "EditPolicy");
            if (!authorization.Succeeded)
            {
                return Unauthorized();
            }
            var albumViewModel = _albumViewModelService.GetAlbumViewModel(album);

            return this.View(albumViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AlbumViewModel albumViewModel)
        {

            if (ModelState.IsValid)
            {
                var album = await _albumService.GetAlbumById(id);
                if (album == null)
                {
                    return NotFound();
                }
                var authorization = await _authorizationService.AuthorizeAsync(User, album, "EditPolicy");
                if (!authorization.Succeeded)
                {
                    return Unauthorized();
                }
                albumViewModel = await _albumViewModelService.UpdateAlbum(album, albumViewModel);
            }
            return this.View(albumViewModel);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _albumService.GetAlbumById(id);
            if (album == null)
            {
                return NotFound();
            }
            var authorization = await _authorizationService.AuthorizeAsync(User, album, "EditPolicy");
            if (!authorization.Succeeded)
            {
                return Unauthorized();
            }
            var albumViewModel = _albumViewModelService.GetAlbumViewModel(album);
            return this.View(albumViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AlbumViewModel viewModel)
        {
            var album = await _albumService.GetAlbumById(viewModel.Id);
            var authorization = await _authorizationService.AuthorizeAsync(User, album, "EditPolicy");
            if (!authorization.Succeeded)
            {
                return Unauthorized();
            }
            await _albumService.RemoveAlbum(album);
            return RedirectToAction("Index", "Profile");
        }
    }
}
