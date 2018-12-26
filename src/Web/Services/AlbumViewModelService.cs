using Inkett.ApplicationCore.Entitites;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Specifications;
using Inkett.Web.Interfaces.Services;
using Inkett.Web.Viewmodels.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Services
{
    public class AlbumViewModelService: IAlbumViewModelService
    {
        private readonly IAsyncRepository<Album> _albumRepository;
        public AlbumViewModelService(IAsyncRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
        }
        public async Task<EditAlbumViewModel> GetEditViewModel(int profileId,int albumId)
        {
            var spec = new AlbumByProfileSpecification(albumId, profileId);
            var album = await _albumRepository.GetSingleBySpec(spec);
            if (album == null)
            {
                return null;
            }
            var viewModel = new EditAlbumViewModel
            {
                Title = album.Title,
                Description = album.Description,
                AlbumPictureUri = album.AlbumPictureUri
            };
            return viewModel;
        }
    }
}
