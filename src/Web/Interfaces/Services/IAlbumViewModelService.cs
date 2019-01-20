using Inkett.ApplicationCore.Entitites;

namespace Inkett.Web.Interfaces.Services
{
    public interface IAlbumViewModelService
    {
        TDestination GetAlbumViewModel<TDestination>(Album album);
    }
}
