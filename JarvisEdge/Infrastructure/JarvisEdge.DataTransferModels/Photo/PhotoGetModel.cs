using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.DataTransferModels.Photo
{
    public class PhotoGetModel
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string Path { get; set; }
    }
}
