namespace JarvisEdge.Services
{
    using ImageMagick;
    using JarvisEdge.DataTransferModels.File;
    using JarvisEdge.ServiceInterfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    public class FileService : IFileService
    {
        #region "Private"
        private readonly IHostingEnvironment hostingEnvironment;
        #endregion

        #region "Constructor"
        public FileService(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
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

        public async Task<string> GenerateFileSource(IFormFile avatarFile, string folderName, string specialFolderName)
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

            if (string.IsNullOrWhiteSpace(hostingEnvironment.WebRootPath))
            {
                hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            if (specialFolderName != null && specialFolderName.Length >= 1)
            {
                uploads = Path.Combine(hostingEnvironment.WebRootPath, folderName + "\\" + specialFolderName);
            }
            else
            {
                uploads = Path.Combine(hostingEnvironment.WebRootPath, folderName);
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
