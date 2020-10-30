using MusicApi.Models;
using System.Threading.Tasks;

namespace MusicApi.Domain.Interfaces
{
    /// <summary>
    /// Performs file operations for the domain
    /// </summary>
    public interface IFileManager
    {

        /// <summary>
        /// Tries to save the track to disk and returns if the action was successful.
        /// </summary>
        /// <param name="track"></param>
        /// <returns>TRUE if successful. FALSE if unsuccessful.</returns>
        Task<bool> SaveTrackToDisk(Track track);

        /// <summary>
        /// Tries to save the image to disk and returns if the action was successful.
        /// </summary>
        /// <param name="track"></param>
        /// <returns>TRUE if successful. FALSE if unsuccessful.</returns>
        Task<bool> SaveImageToDisk(Track track);

        /// <summary>
        /// Tries to delete the track and image file if it exists.
        /// </summary>
        /// <param name="track"></param>
        /// <returns>TRUE if no errors. FALSE if error occurred.</returns>
        bool DeleteIfExists(Track track);

    }
}
