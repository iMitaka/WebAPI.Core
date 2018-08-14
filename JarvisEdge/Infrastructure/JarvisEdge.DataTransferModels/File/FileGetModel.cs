namespace JarvisEdge.DataTransferModels.File
{
    using System.IO;

    public class FileGetModel
    {
        public FileStream File { get; set; }
        public string FileType { get; set; }
        public byte[] FileArray { get; set; }
    }
}
