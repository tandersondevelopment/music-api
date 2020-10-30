namespace MusicApi.Domain.Interfaces
{
    /// <summary>
    /// Holds basic configuration info that has been pulled from the Secret Manager
    /// </summary>
    public interface ILocalConfigurations
    {

        string BaseUrl { get; set; }

        string TracksDirectory { get; set; }

        string ImagesDirectory { get; set; }

        string SecurityToken { get; set; }

    }
}
