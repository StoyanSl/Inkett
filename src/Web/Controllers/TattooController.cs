﻿using Inkett.ApplicationCore.Interfaces.Services;
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
        public async Task<IActionResult> LikeTattoo([FromBody]LikeBindingModel likeModel)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }
            var profileId = _userManager.GetProfileId(User);
            await _tattooService.CreateLike(profileId, likeModel.TattooId);
            return Ok();
        }

        [Route("DislikeTattoo")]
        [HttpPost]
        public async Task<IActionResult> DislikeTattoo([FromBody]LikeBindingModel likeModel)
        {
            if (!ModelState.IsValid)
            {
                return NoContent();
            }
            var profileId = _userManager.GetProfileId(User);
            await _tattooService.RemoveLike(profileId, likeModel.TattooId);
            return Ok();
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
                return NotFound();
            }
            return View(viewModelTask.GetAwaiter().GetResult());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditTattooViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var profileId = _userManager.GetProfileId(User);
                var tattoo = await _tattooService.GetTattooWithStyles(id);
                await _tattooViewModelService.EditTattooByViewModel(viewModel, tattoo);
            }
            return View(viewModel);
        }
    }
}
