using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.Photo;
using JarvisEdge.Models;
using JarvisEdge.ServiceInterfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
            if (model.File != null)
            {
                var photo = new Photo();
                string imgFolder = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), propertyId.ToString());
                var uploadedImgPath = await GenerateFileSource(model.File, propertyId.ToString(), null);
                string waterMarkedImagePath = Path.Combine(imgFolder, Guid.NewGuid().ToString() + "." + uploadedImgPath.Split('.')[1]);
                WaterMarkImage(Path.Combine(imgFolder, uploadedImgPath), waterMarkedImagePath, "FARA IMOTI");
                string imgPath = Guid.NewGuid().ToString() + ".jpeg";
                string optimizedImgPath = Path.Combine(imgFolder, imgPath);
                CompressImage(waterMarkedImagePath, optimizedImgPath);
                photo.Path = imgPath;
                photo.PropertyId = propertyId;
                photo.IsVisiable = true;
                data.Photos.Add(photo);

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

        public bool DeletePhoto(int photoId)
        {
            var photo = data.Photos.All().FirstOrDefault(x => !x.Deleted && x.Id == photoId);

            if (photo != null)
            {
                photo.Deleted = true;
                data.SaveChanges();
                var path = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), photo.PropertyId.ToString());
                File.Delete(Path.Combine(path, photo.Path));
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
                uploads = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), propertyId);
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

        private void WaterMarkImage(string imgPath, string savePath, string waterMarkText)
        {
            using (var img = Image.FromFile(imgPath))
            {
                using (var graphic = Graphics.FromImage(img))
                {
                    PrivateFontCollection myFonts = new PrivateFontCollection();
                    myFonts.AddFontFile(Path.Combine(Directory.GetCurrentDirectory(), "Delija.ttf"));
                    var font = new Font(myFonts.Families[0], img.Width / 4, FontStyle.Regular, GraphicsUnit.Pixel);
                    var color = Color.FromArgb(55, Color.DeepPink);
                    var brush = new SolidBrush(color);
                    var point = new Point(img.Width / 2 - (img.Width / 2 - (img.Width / 30)), img.Height / 2 - (img.Height / 10));

                    graphic.DrawString(waterMarkText, font, brush, point);
                    img.Save(savePath);
                }
            }

            File.Delete(imgPath);
        }

        private void CompressImage(string imgPath, string savePath)
        {
            int size = 1300;
            int quality = 100;

            using (var image = Image.FromFile(imgPath))
            {
                int width, height;
                if (image.Width > image.Height)
                {
                    width = size;
                    height = Convert.ToInt32(image.Height * size / (double)image.Width);
                }
                else
                {
                    width = Convert.ToInt32(image.Width * size / (double)image.Height);
                    height = size;
                }
                var resized = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resized))
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, width, height);
                    using (var output = File.Open(savePath, FileMode.Create))
                    {
                        var qualityParamId = System.Drawing.Imaging.Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                        var codec = ImageCodecInfo.GetImageDecoders()
                            .FirstOrDefault(x => x.FormatID == ImageFormat.Jpeg.Guid);
                        resized.Save(output, codec, encoderParameters);
                    }
                }
            }

            File.Delete(imgPath);
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
