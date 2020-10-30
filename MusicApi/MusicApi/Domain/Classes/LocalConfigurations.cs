using Microsoft.Extensions.Configuration;
using MusicApi.Domain.Interfaces;

namespace MusicApi.Domain.Classes
{
    ///<inheritdoc/>
    public class LocalConfigurations: ILocalConfigurations
    {

        public string BaseUrl { get; set;  }

        public string ImagesDirectory { get; set; }

        public string TracksDirectory { get; set; }    
        
        public string SecurityToken { get; set; }

        public LocalConfigurations(IConfiguration configuration)
        {
            BaseUrl = configuration[Constants.UrlConfigKey];
            ImagesDirectory = configuration[Constants.ImagesConfigKey];
            TracksDirectory = configuration[Constants.TracksConfigKey];
            SecurityToken = configuration[Constants.TokenConfigKey];
        }

    }
}
