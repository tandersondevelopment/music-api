using MusicApi.Domain.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace MusicApi.Domain.Wrappers
{
    ///<inheritdoc/>
    public class FileWrapper : IFileWrapper
    {

        public bool Exists(string path)
            => File.Exists(path);

        public void Delete(string path)
            => File.Delete(path);

        public async Task WriteAllBytesAsync(string path, byte[] bytes)
            => await File.WriteAllBytesAsync(path, bytes);

    }
}
