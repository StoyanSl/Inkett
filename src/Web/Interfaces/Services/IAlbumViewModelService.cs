using Inkett.Web.Viewmodels.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Interfaces.Services
{
    public interface IAlbumViewModelService
    {
        Task<EditAlbumViewModel> GetEditViewModel(int profileId, int albumId);
    }
}
