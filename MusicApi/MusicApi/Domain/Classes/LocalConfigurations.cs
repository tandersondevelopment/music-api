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
            BaseUrl = configuration["Music:BaseUrl"];
            ImagesDirectory = configuration["Music:ImagesDirectory"];
            TracksDirectory = configuration["Music:TracksDirectory"];
            SecurityToken = configuration["Music:SecurityToken"];
        }

    }
}
