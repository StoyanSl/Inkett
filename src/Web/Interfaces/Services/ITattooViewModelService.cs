using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Viewmodels.Profile;
using Inkett.Web.Viewmodels.Tattoo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inkett.Web.Interfaces.Services
{
    public interface ITattooViewModelService
    {
        Task<TattooViewModel> GetCreateTattooViewModel(int profileId);
        Task<TattooViewModel> GetEditTattooViewModel(Tattoo tattoo);
        Task<IndexTattooViewModel> GetIndexTattooViewModel(Tattoo tattoo,int profileId);
        Task CreateTattooByViewModel(TattooViewModel createTattooViewModel, int profileId);
        ListedTattooViewModel GetListedTattooViewModel(Tattoo tattoo);
        List<ListedTattooViewModel> GetListedTattooViewModel(IReadOnlyCollection<Tattoo> tattoos);
        CommentViewModel GetCommentViewModel(ProfileViewModel profileViewModel,string text);
    }
}
