using AutoMapper;
using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Models.InputModels.Tattoos;
using Inkett.Web.Models.ViewModels;
using Inkett.Web.Models.ViewModels.Profiles;
using Inkett.Web.Models.ViewModels.Tattoos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Profile = Inkett.ApplicationCore.Entitites.Profile;
namespace Inkett.Web.Services
{
    public class TattooViewModelService : ITattooViewModelService
    {
        private readonly ITattooService _tattooService;
        private readonly IStyleService _styleService;

        public TattooViewModelService(ITattooService tattooService,
            IStyleService styleService)
        {
            _tattooService = tattooService;
            _styleService = styleService;
        }
        public async Task CreateTattooByViewModel(TattooCreateInputModel inputModel, int profileId)
        {
            var styleIds = inputModel.StylesCheckBoxes.Where(x => x.Checked == true).Select(x => x.Value).ToList();
            var description = inputModel.Description;
            var picture = inputModel.TattooPicture;
            var album = inputModel.Album;

            await _tattooService.CreateTattoo(description, picture, styleIds, profileId, album);
        }

        public async Task<TattooCreateInputModel> GetTattooCreateViewModel(Profile profile)
        {
            var styles = await _styleService.GetStyles();
            var viewModel = Mapper.Map<Profile, TattooCreateInputModel>(profile);
            viewModel.StylesCheckBoxes = Mapper.Map<IReadOnlyCollection<Style>, List<StyleCheckboxModel>>(styles);
            return viewModel;
        }

        public async Task<TattooEditViewModel> GetTattooEditViewModel(Tattoo tattoo)
        {
            var styles =await _styleService.GetStyles();
            var viewModel = Mapper.Map<Tattoo, TattooEditViewModel>(tattoo);
            viewModel.StylesCheckBoxes = Mapper.Map<IReadOnlyCollection<Style>, List<StyleCheckboxModel>>(styles);
            return viewModel;
        }

        public TattooIndexViewModel GetTattooIndexViewModel(Tattoo tattoo, int userProfileId)
        {
            var viewModel = Mapper.Map<Tattoo, TattooIndexViewModel>(tattoo);
            if (viewModel.Profile.Id == userProfileId)
            {
                viewModel.IsOwner = true;
            }
            if (tattoo.Likes.Any(x=>x.ProfileId==userProfileId))
            {
                viewModel.IsLiked = true;
            }
            return viewModel;
        }

        public async Task EditTattooByViewModel(TattooEditInputModel viewModel, Tattoo tattoo)
        {
            var selectedStyles = viewModel.StylesCheckBoxes.Where(x => x.Checked == true).Select(s => s.Value).ToList();
            await _tattooService.EditTattoo(tattoo, viewModel.Description, selectedStyles, viewModel.Album);
        }

        public Task<TattooEditViewModel> GetTattooEditViewModel(TattooEditInputModel inputModel, Profile profile)
        {
            var viewModel = Mapper.Map<TattooEditInputModel, TattooEditViewModel>(inputModel);
            viewModel.Albums = Mapper.Map<List<Album>, List<SelectListItem>>(profile.Albums);
            viewModel.Albums.Add(new SelectListItem() { Text="None",Value="0",Selected=true});
            return Task.FromResult(viewModel);
        }

        public TattooListedViewModel GetTattooListedViewModel(Tattoo tattoo)
        {
            return Mapper.Map<Tattoo, TattooListedViewModel>(tattoo);
        }

        public CommentViewModel GetCommentViewModel(Profile profile, string text)
        {
            var commentViewModel = new CommentViewModel();
            commentViewModel.Profile = Mapper.Map<Profile, ProfileViewModel>(profile);
            commentViewModel.Text = text;
            return commentViewModel;
        }
    }
}
