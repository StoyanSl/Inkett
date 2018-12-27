using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Inkett.ApplicationCore.Specifications;
using Inkett.ApplicationCore.Interfaces.Repositories;
using System.Linq;
using System;
using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Interfaces.Services;
using Microsoft.AspNetCore.Routing;
using Inkett.Web.Viewmodels.Album;
using Microsoft.AspNetCore.Authorization;

namespace Inkett.Web.Controllers
{
    public class AlbumController:Controller
    {
        private readonly InkettUserManager _userManager;
        private readonly IAlbumService _albumService;
        private readonly IProfileService _profileService;
        private readonly IAlbumViewModelService _albumViewModelService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IAsyncRepository<Album> _asyncRepository;

        public AlbumController(
            InkettUserManager userManager,
            IAlbumService albumService,
            IProfileService profileService,
            IAlbumViewModelService albumViewModelService,
            IAuthorizationService authorizationService,
            IAsyncRepository<Album> asyncRepository)
        {
            _albumService = albumService;
            _profileService = profileService;
            _userManager = userManager;
            _albumViewModelService = albumViewModelService;
            _authorizationService = authorizationService;
            _asyncRepository = asyncRepository;
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
                var profileId =  _userManager.GetProfileId(User);
                await _albumService.CreateAlbum(profileId,bindingModel.Title,bindingModel.Description,bindingModel.AlbumPicture);
            }
            return View(bindingModel);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var album = await _asyncRepository.GetByIdAsync(id);
            var result = await _authorizationService.AuthorizeAsync(User, album, "EditPolicy");
            var profileId = _userManager.GetProfileId(User);
            var albumvm = await _albumViewModelService.GetAlbumViewModel(profileId, id);
            if (albumvm is null)
            {
                throw new ApplicationException();
            }
            return this.View(album);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AlbumViewModel editAlbumBindingModel)
        {
            
            var profileId = _userManager.GetProfileId(User);
            await _albumService.EditAlbum(profileId, id, editAlbumBindingModel.Title,
                editAlbumBindingModel.Description,
                editAlbumBindingModel.AlbumPicture);
            return RedirectToAction("Edit", id);
        }

    }
}
