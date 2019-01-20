using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.Web.Interfaces.Services;
using AutoMapper;

namespace Inkett.Web.Services
{
    public class AlbumViewModelService: IAlbumViewModelService
    {
        private readonly IAlbumService _albumService;
        public AlbumViewModelService(IAsyncRepository<Album> albumRepository,
           IAlbumService albumService)
        {
            _albumService = albumService;
           
        }

        public TDestination GetAlbumViewModel<TDestination>(Album album)
        {
            return Mapper.Map<Album, TDestination>(album);
        }

        
    }
}
