using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
        public string Path { get; set; }
        public int Order { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public bool IsVisiable { get; set; }
    }
}
