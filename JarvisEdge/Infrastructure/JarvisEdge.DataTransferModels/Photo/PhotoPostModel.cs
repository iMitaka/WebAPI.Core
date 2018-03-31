using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.DataTransferModels.Photo
{
    public class PhotoPostModel
    {
        public ICollection<IFormFile> Files { get; set; }
    }
}
