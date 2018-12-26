
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Inkett.ApplicationCore.Interfaces.Services;
using System;
using System.IO;

namespace Inkett.Infrastructure.Services
{
    public class ImageUploader : IImageUploader
    {
        private const string cloudName = "";
        private const string apiKey = "";
        private const string apiSecret = "";

        private readonly Cloudinary _cloudinary;

        public ImageUploader()
        {
            Account account = new Account(
                                       cloudName,
                                       apiKey,
                                       apiSecret);

            _cloudinary = new Cloudinary(account);

        }
        public UploadResult ImageUpload(Stream stream)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(),stream)
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            return uploadResult;
        }


    }
}
