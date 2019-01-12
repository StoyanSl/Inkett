using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.ApplicationCore.Specifications;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Album;
using Inkett.Web.Viewmodels.Tattoo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Services
{
    public class AlbumViewModelService: IAlbumViewModelService
    {
        private readonly IAlbumService _albumService;

        public AlbumViewModelService(IAsyncRepository<Album> albumRepository,
           IAlbumService albumService
           )
        {
            _albumService = albumService;
           
        }
        
        public  AlbumViewModel GetAlbumViewModel(Album album)
        {
            return new AlbumViewModel
            {
                Id = album.Id,
                Title = album.Title,
                Description = album.Description,
                PictureUri = album.AlbumPictureUri
            };
        }

        public Task<AlbumViewModel> GetAlbumViewModel(int albumId)
        {
            throw new NotImplementedException();
        }

        public AlbumIndexViewModel GetIndexAlbumViewModel(Album album)
        {
            var viewModel = new AlbumIndexViewModel();
            viewModel.Id = album.Id;
            viewModel.Title = album.Title;
            viewModel.Description = album.Description;
            foreach (var tattoo in album.Tattoos)
            {
                viewModel.Tattoos.Add(GetListedTattooViewModel(tattoo));
            }
            return viewModel;
        }

        public async Task<AlbumViewModel> UpdateAlbum(Album album, AlbumViewModel albumViewModel)
        {
            var editedAlbum = await _albumService.EditAlbum(album,albumViewModel.Title, albumViewModel.Picture);
            return GetAlbumViewModel(editedAlbum);
        }
        private ListedTattooViewModel GetListedTattooViewModel(Tattoo tattoo)
        {
            return new ListedTattooViewModel()
            {
                Id = tattoo.Id,
                PictureUri = tattoo.TattooPictureUri
            };
        }
    }
}
