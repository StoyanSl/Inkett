using Inkett.Web.Viewmodels.Tattoo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Interfaces.Services
{
    public interface ITattooViewModelService
    {
        Task<CreateTattooViewModel> GetCreateTattooViewModelAsync(string accountId);
        Task CreateTattooByViewModel(CreateTattooViewModel createTattooViewModel, int profileId);
    }
}
