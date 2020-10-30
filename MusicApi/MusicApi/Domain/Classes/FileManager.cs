using MusicApi.Domain.Interfaces;
using MusicApi.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MusicApi.Domain.Classes
{
    ///<inheritdoc/>
    public class FileManager : IFileManager
    {

        #region Fields

        private readonly ILocalConfigurations _localConfigurations;
        private readonly IFileWrapper _fileWrapper;

        #endregion

        #region Constructors

        public FileManager(
            ILocalConfigurations localConfigurations
            , IFileWrapper fileWrapper)
        {
            _localConfigurations = localConfigurations;
            _fileWrapper = fileWrapper;
        }

        #endregion

        #region IFileManager Implementations

        ///<inheritdoc/>
        public async Task<bool> SaveTrackToDisk(Track track)
            => await SaveDataToDisk(GetTrackPath(track), track.FileData);

        ///<inheritdoc/>
        public async Task<bool> SaveImageToDisk(Track track)
            => await SaveDataToDisk(GetImagePath(track), track.ImageData);

        ///<inheritdoc/>
        public bool DeleteIfExists(Track track)
        {
            try
            {
                DeleteIfExists(GetTrackPath(track));
                DeleteIfExists(GetImagePath(track));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Helper Methods

        // TODO: change folders based on track type.
        private string GetTrackPath(Track track)
            => Path.Combine(_localConfigurations.TracksDirectory, track.FileName);

        // TODO: change folders based on track type.
        private string GetImagePath(Track track)
            => Path.Combine(_localConfigurations.ImagesDirectory,
                $"{Path.GetFileNameWithoutExtension(track.FileName)}.jpg");

        // TODO: create folder if not exists.        
        private async Task<bool> SaveDataToDisk(string path, byte[] data)
        {
            try
            {
                DeleteIfExists(path);
                await _fileWrapper.WriteAllBytesAsync(path, data);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void DeleteIfExists(string path)
        {
            if (_fileWrapper.Exists(path))
            {
                _fileWrapper.Delete(path);
            }
        }

        #endregion

    }
}
