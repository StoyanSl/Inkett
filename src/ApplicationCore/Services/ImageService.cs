using Inkett.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Inkett.ApplicationCore.Services.Results;

namespace Inkett.ApplicationCore.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageUploader _imageUploader;
        public ImageService(IImageUploader imageUploader)
        {
            _imageUploader = imageUploader;
        }
        public ImageUploadResult UploadImage(IFormFile formFile)
        {
            ImageUploadResult imageUploadResult = new ImageUploadResult();

            using (var stream = formFile.OpenReadStream())
            {
                var uploadResult = _imageUploader.ImageUpload(stream);
                if (uploadResult.Error != null)
                {
                    imageUploadResult.Success = false;
                    return imageUploadResult;
                }
                imageUploadResult.ImageUri = uploadResult.SecureUri.ToString();
                imageUploadResult.Success = true;
            }
            return imageUploadResult;


        }
    }
}
