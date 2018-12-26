using Inkett.ApplicationCore.Services.Results;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface IImageService
    {
       ImageUploadResult UploadImage(IFormFile formFile);
    }
}
