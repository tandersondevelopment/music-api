using System.Threading.Tasks;

namespace MusicApi.Domain.Interfaces
{
    /// <summary>
    /// Wraps the System.IO.File operations for DI and mocking usage.
    /// </summary>
    public interface IFileWrapper
    {

        bool Exists(string path);

        void Delete(string path);

        Task WriteAllBytesAsync(string path, byte[] bytes);

    }
}
