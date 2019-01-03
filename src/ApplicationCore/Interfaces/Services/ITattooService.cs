﻿using Inkett.ApplicationCore.Entitites;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface ITattooService
    {
        Task CreateTattoo(string description, IFormFile tattooPicture, IEnumerable<int> styleIds,int profileId,int albumId);
        Task<Tattoo> GetTattooById(int id);
        Task<Tattoo> GetTattooWithStyles(int id);
    }
}
