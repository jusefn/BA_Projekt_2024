using AppClassLibrary.Enums;

namespace AppClassLibrary.Models
{
    public class FileObject
    {
        public string? Content { get; set; }
        public ContentTypeEnum ContentType { get; set; }
    }
}
