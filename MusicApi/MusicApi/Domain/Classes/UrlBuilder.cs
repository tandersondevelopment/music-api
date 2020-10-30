using MusicApi.Domain.Interfaces;
using MusicApi.Models;
using System.IO;

namespace MusicApi.Domain.Classes
{
    ///<inheritdoc/>
    public class UrlBuilder : IUrlBuilder
    {

        #region Fields

        private readonly ILocalConfigurations _localConfigurations;

        #endregion

        #region Constructors

        public UrlBuilder(
            ILocalConfigurations localConfigurations)
        {
            _localConfigurations = localConfigurations;
        }

        #endregion

        #region Interface Implementation

        public string GetTrackUrl(Track track)
            => $"{_localConfigurations.BaseUrl.TrimEnd('/')}/tracks/{track.FileName}";


        public string GetImageUrl(Track track)
            => $"{_localConfigurations.BaseUrl.TrimEnd('/')}/tracks/{Path.GetFileNameWithoutExtension(track.FileName)}.jpg";

        #endregion

    }
}
