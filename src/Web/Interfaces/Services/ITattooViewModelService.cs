using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Models.InputModels.Tattoos;
using Inkett.Web.Models.ViewModels.Tattoos;
using System.Threading.Tasks;

namespace Inkett.Web.Interfaces.Services
{
    public interface ITattooViewModelService
    {
        //Task<TattooViewModel> GetCreateTattooViewModel(int profileId);
        //Task<EditTattooViewModel> GetEditTattooViewModel(Tattoo tattoo);
        //Task<IndexTattooViewModel> GetIndexTattooViewModel(Tattoo tattoo, int profileId);
        //Task CreateTattooByViewModel(TattooViewModel createTattooViewModel, int profileId);
        //Task EditTattooByViewModel(EditTattooViewModel editTattooViewModel, Tattoo tattoo);
        //ListedTattooViewModel GetListedTattooViewModel(Tattoo tattoo);
        //List<ListedTattooViewModel> GetListedTattooViewModel(IReadOnlyCollection<Tattoo> tattoos);
    

        TattooIndexViewModel GetTattooIndexViewModel(Tattoo tattoo, int userProfileId);
        TattooListedViewModel GetTattooListedViewModel(Tattoo tattoo);
        Task<TattooCreateInputModel> GetTattooCreateViewModel(Profile profile);
        Task<TattooEditViewModel> GetTattooEditViewModel(Tattoo tattoo);
        Task<TattooEditViewModel> GetTattooEditViewModel(TattooEditInputModel inputModel,Profile tattoo);
        Task CreateTattooByViewModel(TattooCreateInputModel createTattooInputModel, int profileId);
        Task EditTattooByViewModel(TattooEditInputModel viewModel, Tattoo tattoo);
        CommentViewModel GetCommentViewModel(Profile profile, string text);

    }
}
