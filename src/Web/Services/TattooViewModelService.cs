using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Specifications;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Tattoo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Inkett.ApplicationCore.Interfaces.Services;

namespace Inkett.Web.Services
{
    public class TattooViewModelService: ITattooViewModelService
    {
        private readonly IAsyncRepository<Style> _styleRepository;
        private readonly IAsyncRepository<Profile> _profileRepository;
        private readonly ITattooService _tattooService;
        public TattooViewModelService(IAsyncRepository<Style> styleRepository,
            IAsyncRepository<Profile> profileRepository,
            ITattooService tattooService)
        {
            _styleRepository = styleRepository;
            _profileRepository = profileRepository;
            _tattooService = tattooService;
        }
        
        public async Task<CreateTattooViewModel> GetCreateTattooViewModelAsync(string accountId)
        {
            var styles = await _styleRepository.ListAllAsync();
            var profile = await _profileRepository.GetSingleBySpec(new ProfileByAccountIdSpecification(accountId));
            var profileWithAlbums = await _profileRepository.GetSingleBySpec(new ProfileWithAlbumsSpecification(profile.Id));
            var viewModel= new CreateTattooViewModel();
            foreach (var style in styles)
            {
                viewModel.StylesCheckBoxes.Add(new Viewmodels.CheckboxModel()
                {
                    Value = style.Id,
                    Text = style.Name,
                    Checked = false
                });
            }
            foreach (var album in profileWithAlbums.Albums)
            {
                viewModel.Albums.Add(new SelectListItem()
                {
                    Value = album.Id.ToString(),
                    Text=album.Title
                });
            }

            return viewModel;
        }

        public async Task CreateTattooByViewModel(CreateTattooViewModel viewModel, int profileId)
        {
            var selectedStyles = new List<int>();
            foreach (var selectedStyle in viewModel.StylesCheckBoxes.Where(s=>s.Checked==true))
            {
                selectedStyles.Add(selectedStyle.Value);
            }
           await _tattooService.CreateTattoo(viewModel.Description,viewModel.TattooPicture,selectedStyles,profileId,viewModel.Album);
        }
    }
}
