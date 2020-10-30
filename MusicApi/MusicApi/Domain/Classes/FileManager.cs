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
        {
            var trackPath = Path.Combine(_localConfigurations.TracksDirectory, track.FileName);
            return await SaveDataToDisk(trackPath, track.FileData);
        }

        ///<inheritdoc/>
        public async Task<bool> SaveImageToDisk(Track track)
        {
            var trackPath = Path.Combine(_localConfigurations.ImagesDirectory,
                $"{Path.GetFileNameWithoutExtension(track.FileName)}.jpg");
            return await SaveDataToDisk(trackPath, track.ImageData);
        }

        #endregion

        #region Helper Methods

        private async Task<bool> SaveDataToDisk(string path, byte[] data)
        {
            try
            {
                if (_fileWrapper.Exists(path))
                {
                    _fileWrapper.Delete(path);
                }

                await _fileWrapper.WriteAllBytesAsync(path, data);                
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}
