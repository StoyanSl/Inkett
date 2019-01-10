using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Tattoo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Web.Viewmodels;
using Inkett.Web.Viewmodels.Profile;

namespace Inkett.Web.Services
{
    public class TattooViewModelService : ITattooViewModelService
    {
        private const string notSelectedAlbum = "None";
        private const int notSelectedAlbumId = 0;

        private readonly IAsyncRepository<Style> _styleRepository;
        private readonly IAsyncRepository<Profile> _profileRepository;
        private readonly IProfileService _profileService;
        private readonly ITattooService _tattooService;
        private readonly IStyleService _styleService;


        public TattooViewModelService(IAsyncRepository<Style> styleRepository,
            IAsyncRepository<Profile> profileRepository,
            ITattooService tattooService,
            IStyleService styleService,
            IProfileService profileService)
        {
            _styleRepository = styleRepository;
            _profileRepository = profileRepository;
            _tattooService = tattooService;
            _styleService = styleService;
            _profileService = profileService;
        }

        public async Task<IndexTattooViewModel> GetIndexTattooViewModel(Tattoo tattoo, int profileId)
        {

            var viewModel = new IndexTattooViewModel
            {
                Id = tattoo.Id,
                Description = tattoo.Description,
                PictureUri = tattoo.TattooPictureUri

            };
            viewModel.Comments = tattoo.Comments.Select(c => new CommentViewModel()
            {
                Profile = new ProfileViewModel()
                {
                    ProfileName = c.Profile.ProfileName,
                    Id = c.ProfileId,
                    ProfilePictureUri = c.Profile.ProfilePicture,
                },
                Text = c.Text
            }).ToList();

            var styles = await _styleService.GetStyles();
            foreach (var tattooStyle in tattoo.TattooStyles)
            {
                viewModel.Styles.Add(new StyleViewModel()
                {
                    Id = tattooStyle.StyleId,
                    Name = styles.First(x => x.Id == tattooStyle.StyleId).Name
                });
            }
            viewModel.IsLiked = tattoo.Likes.Any(l => l.ProfileId == profileId);
            viewModel.IsOwner = tattoo.ProfileId == profileId ? true : false;
            return viewModel;
        }

        public async Task<TattooViewModel> GetCreateTattooViewModel(int profileId)
        {
            var stylesTask = _styleService.GetStyles();
            var profileWithAlbumsTask = _profileService.GetProfileWithAlbums(profileId);

            var viewModel = new TattooViewModel();

            var styles = await stylesTask;
            viewModel.StylesCheckBoxes = await GetStylesCheckboxes();

            var profileWithAlbums = await profileWithAlbumsTask;
            viewModel.Albums = GetAlbumsSelectList(profileWithAlbums.Albums);

            return viewModel;
        }

        public async Task CreateTattooByViewModel(TattooViewModel viewModel, int profileId)
        {
            var selectedStyles = new List<int>();
            foreach (var selectedStyle in viewModel.StylesCheckBoxes.Where(s => s.Checked == true))
            {
                selectedStyles.Add(selectedStyle.Value);
            }
            await _tattooService.CreateTattoo(viewModel.Description, viewModel.TattooPicture, selectedStyles, profileId, viewModel.Album);
        }

        public async Task EditTattooByViewModel(EditTattooViewModel viewModel, Tattoo tattoo)
        {
            var selectedStyles = new List<int>();
            foreach (var selectedStyle in viewModel.StylesCheckBoxes.Where(s => s.Checked == true))
            {
                selectedStyles.Add(selectedStyle.Value);
            }
            await _tattooService.EditTattoo(tattoo, viewModel.Description, selectedStyles, viewModel.Album);
        }
        
        public ListedTattooViewModel GetListedTattooViewModel(Tattoo tattoo)
        {
            return new ListedTattooViewModel()
            {
                Id = tattoo.Id,
                PictureUri = tattoo.TattooPictureUri
            };
        }

        public async Task<EditTattooViewModel> GetEditTattooViewModel(Tattoo tattoo)
        {
            var styles = await _styleService.GetStyles();
            var viewModel = new EditTattooViewModel();
            viewModel.Album = tattoo.AlbumId?? 0;
            viewModel.PictureUri = tattoo.TattooPictureUri;
            viewModel.Description = tattoo.Description;
            viewModel.StylesCheckBoxes = await GetStylesCheckboxes(tattoo.TattooStyles);
            viewModel.Albums = GetAlbumsSelectList(tattoo.Profile.Albums, tattoo.AlbumId);
            return viewModel;
        }

        public CommentViewModel GetCommentViewModel(ProfileViewModel profileViewModel, string text)
        {
            return new CommentViewModel() { Profile = profileViewModel, Text = text };


        }

        private async Task<List<CheckboxModel>> GetStylesCheckboxes()
        {
            var styles = await _styleService.GetStyles();
            var stylesCheckBoxes = styles.Select(s => new CheckboxModel()
            {
                Value = s.Id,
                Text = s.Name,
                Checked = false
            }).ToList();
            return stylesCheckBoxes;
        }

        private async Task<List<CheckboxModel>> GetStylesCheckboxes(List<TattooStyle> tattooStyles)
        {
            var checkedStyles = tattooStyles.Select(x => x.StyleId).ToList();
            var styles = await _styleService.GetStyles();
            var stylesCheckBoxes = styles.Select(s => new CheckboxModel()
            {
                Value = s.Id,
                Text = s.Name,
                Checked = checkedStyles.Contains(s.Id)
            }).ToList();
            return stylesCheckBoxes;
        }

        private List<SelectListItem> GetAlbumsSelectList(List<Album> albums, int? selectedAlbumId = null)
        {
            var albumsSelectList = albums.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Title,
                Selected = selectedAlbumId == s.Id
            }).ToList();
            albumsSelectList.Add(new SelectListItem()
            {
                Selected = selectedAlbumId == null ? true : false,
                Text = notSelectedAlbum,
                Value = notSelectedAlbumId.ToString()
            });
            return albumsSelectList;
        }

        public List<ListedTattooViewModel> GetListedTattooViewModel(IReadOnlyCollection<Tattoo> tattoos)
        {
            var colleciton = new List<ListedTattooViewModel>();
            foreach (var tattoo in tattoos)
            {
                colleciton.Add(GetListedTattooViewModel(tattoo));
            }
            return colleciton;
        }
    }
}
