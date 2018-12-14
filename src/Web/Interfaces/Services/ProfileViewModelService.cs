using Inkett.Web.Viewmodels.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Interfaces.Services
{
    public interface IProfileViewModelService
    {
        Task<ProfileViewModel> GetProfileViewModel(string userName);
    }
}
