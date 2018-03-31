using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.Photo;
using JarvisEdge.Models;
using JarvisEdge.ServiceInterfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JarvisEdge.Services
{
    public class FileService : IFileService
    {
        #region "Private"
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IUowData data;
        #endregion

        #region "Constructor"
        public FileService(IHostingEnvironment hostingEnvironment, IUowData data)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.data = data;
        }
        #endregion

        #region "Public methods"
        //public FileGetModel GetImageFile(string imgPath, string folderName, string specialFolderName)
        //{
        //    var model = new FileGetModel();
        //    var imagePath = Path.Combine(hostingEnvironment.ContentRootPath, folderName + "\\" + specialFolderName + '\\' + imgPath);

        //    if (IsImageExist(imagePath))
        //    {
        //        //using (var image = new MagickImage(imagePath))
        //        //{
        //        //    image.Resize(int.Parse(resize[0]), int.Parse(resize[1]));
        //        //    image.Strip();
        //        //    image.Quality = 75;
        //        //    model.FileArray = image.ToByteArray();
        //        //}
        //        model.File = File.OpenRead(imagePath);
        //        model.FileType = Path.GetExtension(imagePath);
        //        return model;
        //    }

        //    return null;
        //}

        public async Task<bool> PostImagesForProperty(int propertyId, PhotoPostModel model)
        {
            if (model.Files != null && model.Files.Any())
            {
                string imgPath = String.Empty;
                var photo = new Photo();
                foreach (var image in model.Files)
                {
                    imgPath = await GenerateFileSource(image, propertyId.ToString(), null);
                    photo.Path = imgPath;
                    photo.PropertyId = propertyId;
                    photo.IsVisiable = true;
                    data.Photos.Add(photo);
                }

                data.SaveChanges();

                return true;
            }

            return false;

        }

        public bool ModifyPhoto(int photoId, int orderNumber)
        {
            var photo = data.Photos.All().FirstOrDefault(x => !x.Deleted && x.Id == photoId);

            if (photo != null)
            {
                photo.Order = orderNumber;
                data.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<string> GenerateFileSource(IFormFile avatarFile, string propertyId, string specialFolderName)
        {
            if (avatarFile == null)
                return null;

            var avatarFileExtension = GetAvatarFileFormat(avatarFile.FileName);

            if (avatarFileExtension == null || avatarFile.Length == 0)
                return null;

            var hash = string.Empty;

            using (var md5 = MD5.Create())
            {
                var stream = avatarFile.OpenReadStream();
                hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
            }

            var uploads = String.Empty;
            if (specialFolderName != null && specialFolderName.Length >= 1)
            {
                uploads = Path.Combine(hostingEnvironment.WebRootPath, propertyId + "\\" + specialFolderName);
            }
            else
            {
                uploads = Path.Combine(hostingEnvironment.WebRootPath, propertyId);
            }

            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            var avatarPath = Path.Combine(uploads, $"{hash}{avatarFileExtension}");

            if (!File.Exists(avatarPath))
            {
                using (var fileStream = new FileStream(avatarPath, FileMode.Create))
                {
                    await avatarFile.CopyToAsync(fileStream);
                }
            }

            return $"{hash}{avatarFileExtension}";
        }
        #endregion

        #region "Private methods"
        private bool IsImageExist(string imagePath)
        {
            return File.Exists(imagePath);
        }

        private static string GetAvatarFileFormat(string fileName)
        {
            string fileFormat = Path.GetExtension(fileName).ToLower();

            switch (fileFormat)
            {
                case ".jpg":
                case ".png":
                case ".gif": return fileFormat;
                default:
                    return null;
            }
        }
        #endregion
    }
}
