
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.ApplicationCore.Services.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Inkett.Infrastructure.Services
{
    public class ImageUploader : IImageUploader
    {
        private readonly CloudinaryApiDetails _cloudinaryApiDetails;
        private readonly Cloudinary _cloudinary;

        public ImageUploader(IOptions<CloudinaryApiDetails> cloudinaryApiDetails)
        {
            _cloudinaryApiDetails = cloudinaryApiDetails.Value;
            Account account = new Account(
                                       _cloudinaryApiDetails.CloudName,
                                        _cloudinaryApiDetails.ApiKey,
                                       _cloudinaryApiDetails.ApiSecret);

            _cloudinary = new Cloudinary(account);

        }

        public UploadResult ImageUpload(Stream stream)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), stream)
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            return uploadResult;
        }

        public UploadResult ImageUpload(string imgPath)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imgPath)
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            return uploadResult;
        }

        private static Bitmap ByteToImage(byte[] blob)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                mStream.Write(blob, 0, blob.Length);
                mStream.Seek(0, SeekOrigin.Begin);

                Bitmap originalImg = new Bitmap(mStream);
                var resizedImg = resizeImage(originalImg);
                return resizedImg;
            }
        }
        private static Bitmap resizeImage(Bitmap imgToResize)
        {
            return new Bitmap(imgToResize,186,186);
        }
        private static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }
    }
}
