﻿using Inkett.ApplicationCore.Entitites;
using Inkett.Web.Viewmodels.Tattoo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inkett.Web.Interfaces.Services
{
    public interface ITattooViewModelService
    {
        Task<TattooViewModel> GetCreateTattooViewModel(int profileId);
        Task<TattooViewModel> GetEditTattooViewModel(Tattoo tattoo);
        Task<IndexTattooViewModel> GetIndexTattooViewModel(Tattoo tattoo);
        Task CreateTattooByViewModel(TattooViewModel createTattooViewModel, int profileId);
        ListedTattooViewModel GetListedTattooViewModel(Tattoo tattoo);
    }
}
