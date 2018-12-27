using Inkett.Web.Viewmodels.Profile;
using System.Threading.Tasks;


namespace Inkett.Web.Interfaces.Services
{
    public interface IProfileViewModelService
    {
        Task<ProfileViewModel> GetProfileViewModel(int profileId);
        Task<EditProfileViewModel> GetEditProfileViewModel(int profileId);
    }
}
