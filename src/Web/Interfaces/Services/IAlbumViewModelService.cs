using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Viewmodels.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Interfaces.Services
{
    public interface IAlbumViewModelService
    {
        AlbumViewModel GetAlbumViewModel(Album album);
        AlbumIndexViewModel GetIndexAlbumViewModel(Album album);
        Task<AlbumViewModel> UpdateAlbum(Album album, AlbumViewModel albumViewModel);
    }
}
