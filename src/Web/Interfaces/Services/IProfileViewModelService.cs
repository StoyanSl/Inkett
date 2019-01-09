using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Viewmodels.Profile;
using System.Threading.Tasks;


namespace Inkett.Web.Interfaces.Services
{
    public interface IProfileViewModelService
    {
        ProfileViewModel GetProfileViewModel(Profile profile);
        EditProfileViewModel GetEditProfileViewModel(Profile profile);
        ProfileAlbumsViewModel GetProfileAlbumsViewModel(Profile profile,int userProfileId);
        ProfileTattoosViewModel GetProfileTattoosViewModel(Profile profile,int userProfileId);
        ProfileTattoosViewModel GetProfileLikedTattoosViewModel(Profile profile,int userProfileId);
    }
}
