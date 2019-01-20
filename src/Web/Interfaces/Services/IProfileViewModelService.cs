using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Models.ViewModels.Profiles;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Inkett.Web.Interfaces.Services
{
    public interface IProfileViewModelService
    {
        List<ProfileViewModel> GetProfilesViewModels(IReadOnlyCollection<Profile> profiles);
        ProfileAlbumsViewModel GetProfileAlbumsViewModel(Profile profile,int userProfileId);
        ProfileTattoosViewModel GetProfileTattoosViewModel(Profile profile,int userProfileId);
        ProfileTattoosViewModel GetProfileLikedTattoosViewModel(Profile profile);
        ProfileIndexViewModel GetProfileIndexViewModel(Profile profile, int userProfileId);
        ProfileViewModel GetProfileViewModel(Profile profile);
        ProfileAlbumViewModel GetProfileAlbumViewModel(Album album, int userProfileId);
    }
}
