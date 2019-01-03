using System.IO;
using CloudinaryDotNet.Actions;

namespace Inkett.ApplicationCore.Interfaces.Services
{
    public interface IImageUploader
    {
        UploadResult ImageUpload(Stream stream);
        UploadResult ImageUpload(string imgPath);
    }
}
