using System.Text;
using System.Text.Json;
using AppClassLibrary.Enums;
using AppClassLibrary.Models;

namespace AppClassLibrary.Handlers
{
    public class FileHandler
    {

        public void SaveFile(string content, string path, ContentTypeEnum type)
        {
            FileObject fileObject = new FileObject();
            fileObject.ContentType = type;
            fileObject.Content = content;
            SaveToFS(fileObject, path);
        }

        private void SaveToFS(FileObject fileObject, string path)
        {
            using (var stream = File.Open(path, FileMode.Create))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    writer.Write(JsonSerializer.SerializeToUtf8Bytes(fileObject));
                }
            }
        }

        public string LoadFile(string path, ContentTypeEnum type)
        {
            FileObject fileObject = LoadFromFS(path);
            if(fileObject.ContentType != type)
            {
                throw new Exception("Content Type is not equal.");
            }
            if (fileObject.Content == null)
            {
                throw new Exception("Content should not be null.");
            }
            return fileObject.Content;
        }

        private FileObject LoadFromFS(string path)
        {
            if (File.Exists(path))
            {
                using (var stream = File.Open(path, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        byte[] contentBytes = reader.ReadBytes((int)stream.Length);
                        var readOnlySpan = new ReadOnlySpan<byte>(contentBytes);

                        return JsonSerializer.Deserialize<FileObject>(readOnlySpan)!;

                    }
                }
            }
            else
            {
                throw new FileNotFoundException("File not found");
            }
        }
    }
}
