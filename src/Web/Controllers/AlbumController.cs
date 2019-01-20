using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Models.InputModels.Albums;
using Inkett.Web.Models.ViewModels.Albums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;


namespace Inkett.Web.Controllers
{
    [Authorize]
    public class AlbumController : Controller
    {
        private readonly InkettUserManager _userManager;
        private readonly IAlbumService _albumService;
        private readonly IAlbumViewModelService _albumViewModelService;
        private readonly IAuthorizationService _authorizationService;

        public AlbumController(
            InkettUserManager userManager,
            IAlbumService albumService,
            IProfileService profileService,
            IAlbumViewModelService albumViewModelService,
            IAuthorizationService authorizationService)
        {
            _albumService = albumService;
            _userManager = userManager;
            _albumViewModelService = albumViewModelService;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlbumCreateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var profileId = _userManager.GetProfileId(User);
                await _albumService.CreateAlbum(profileId, inputModel.Title, inputModel.Description, inputModel.Picture);
                return RedirectToAction("Albums", "Profile");
            }
            return View(inputModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var album = await _albumService.GetAlbumById(id);
            if (album == null)
            {
                return new NotFoundResult();
            }
            var authorization = await _authorizationService.AuthorizeAsync(User, album, "EditPolicy");
            if (!authorization.Succeeded)
            {
                return new ForbidResult();
            }
            var albumViewModel = _albumViewModelService.GetAlbumViewModel<AlbumEditViewModel>(album);
            return this.View(albumViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AlbumEditInputModel albumEditInputModel)
        {
            if (ModelState.IsValid)
            {
                var album = await _albumService.GetAlbumById(id);
                if (album == null)
                {
                    return new NotFoundResult();
                }
                var authorization = await _authorizationService.AuthorizeAsync(User, album, "EditPolicy");
                if (!authorization.Succeeded)
                {
                    return new ForbidResult();
                }
               
            }
            var viewModel = Mapper.Map<AlbumEditInputModel, AlbumEditViewModel>(albumEditInputModel);
            return this.View(viewModel);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await _albumService.GetAlbumById(id);
            if (album == null)
            {
                return new NotFoundResult();
            }
            var authorization = await _authorizationService.AuthorizeAsync(User, album, "EditPolicy");
            if (!authorization.Succeeded)
            {
                return new ForbidResult();
            }
            var albumViewModel = _albumViewModelService.GetAlbumViewModel<AlbumDeleteViewModel>(album);
            return this.View(albumViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AlbumDeleteInputModel inputModel)
        {
            var album = await _albumService.GetAlbumById(inputModel.Id);
            if (album == null)
            {
                return new NotFoundResult();
            }
            var authorization = await _authorizationService.AuthorizeAsync(User, album, "EditPolicy");
            if (!authorization.Succeeded)
            {
                return new ForbidResult();
            }
            await _albumService.RemoveAlbum(album);
            return RedirectToAction("Index", "Profile");
        }
    }
}
