using JarvisEdge.DataTransferModels.Photo;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JarvisEdge.ServiceInterfaces
{
    public interface IFileService
    {
        Task<string> GenerateFileSource(IFormFile avatarFile, string propertyId, string specialFolderName);
        Task<bool> PostImagesForProperty(int propertyId, PhotoPostModel model);
        bool ModifyPhoto(int photoId, int orderNumber);
        bool DeletePhoto(int photoId);
    }
}
