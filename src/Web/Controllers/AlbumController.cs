using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Inkett.Web.Viewmodels.Album.BindingModels;
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

namespace Inkett.Web.Controllers
{
    public class AlbumController:Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAlbumService _albumService;
        private readonly IProfileService _profileService;
        private readonly IAlbumViewModelService _albumViewModelService; 
        public AlbumController(
            UserManager<ApplicationUser> userManager,
            IAlbumService albumService,
            IProfileService profileService,
            IAlbumViewModelService albumViewModelService)
        {
            _albumService = albumService;
            _profileService = profileService;
            _userManager = userManager;
            _albumViewModelService = albumViewModelService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAlbumBindingModel bindingModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var profile = await _profileService.GetProfileByAccountId(user.Id);
                await _albumService.CreateAlbum(profile.Id,bindingModel.Title,bindingModel.Description,bindingModel.AlbumPicture);
            }
            return View(bindingModel);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _profileService.GetProfileByAccountId(user.Id);
            var album = await _albumViewModelService.GetEditViewModel(profile.Id, id);
            if (album is null)
            {
                throw new ApplicationException();
            }
            return this.View(album);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditAlbumBindingModel editAlbumBindingModel)
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _profileService.GetProfileByAccountId(user.Id);
            await _albumService.EditAlbum(profile.Id, id, editAlbumBindingModel.Title,
                editAlbumBindingModel.Description,
                editAlbumBindingModel.AlbumPicture);
            return RedirectToAction("Edit", id);
        }

    }
}
