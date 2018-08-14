namespace JarvisEdge.ServiceInterfaces
{
    using JarvisEdge.DataTransferModels.File;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface IFileService
    {
        Task<string> GenerateFileSource(IFormFile avatarFile, string folderName, string specialFolderName);
        //FileGetModel GetImageFile(string imgPath, string folderName, string specialFolderName);
    }
}
