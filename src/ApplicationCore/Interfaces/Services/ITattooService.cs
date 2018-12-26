using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface ITattooService
    {
        Task CreateTattoo(string description, IFormFile tattooPicture, IEnumerable<int> styleIds,int profileId,int albumId);
    }
}
