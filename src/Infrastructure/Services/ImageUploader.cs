
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Inkett.ApplicationCore.Interfaces.Services;
using System;
using System.IO;

namespace Inkett.Infrastructure.Services
{
    public class ImageUploader : IImageUploader
    {
        private const string cloudName = "inkettimgs";
        private const string apiKey = "291351422639137";
        private const string apiSecret = "EvDuWYGlZFActWoWpxVaVtbrfY8";

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
