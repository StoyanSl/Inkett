using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Viewmodels.Profile;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Inkett.Web.Interfaces.Services
{
    public interface IProfileViewModelService
    {
        ProfileViewModel GetProfileViewModel(Profile profile);
        List<ProfileViewModel> GetProfilesViewModels(IReadOnlyCollection<Profile> profiles);
        EditProfileViewModel GetEditProfileViewModel(Profile profile);
        ProfileAlbumsViewModel GetProfileAlbumsViewModel(Profile profile,int userProfileId);
        ProfileTattoosViewModel GetProfileTattoosViewModel(Profile profile,int userProfileId);
        ProfileTattoosViewModel GetProfileLikedTattoosViewModel(Profile profile,int userProfileId);
    }
}
