using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Viewmodels.Profile;
using System.Threading.Tasks;


namespace Inkett.Web.Interfaces.Services
{
    public interface IProfileViewModelService
    {
        ProfileViewModel GetProfileViewModel(Profile profile);
        EditProfileViewModel GetEditProfileViewModel(Profile profile);
        ProfileAlbumsViewModel GetProfileAlbumsViewModel(Profile profile);
        ProfileTattoosViewModel GetProfileTattoosViewModel(Profile profile);
        ProfileTattoosViewModel GetProfileLikedTattoosViewModel(Profile profile);
    }
}
