using MusicApi.Models;

namespace MusicApi.Domain.Interfaces
{
    /// <summary>
    /// Builds out urls based on where the files where placed on the server.
    /// </summary>
    public interface IUrlBuilder
    {
        string GetTrackUrl(Track track);

        string GetImageUrl(Track track);
    }
}
