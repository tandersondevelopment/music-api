using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MusicApi.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TrackTypes
    {
        Unknown = 0,
        Composition,
        LiveGroup,
        LiveRecording,
        Mix,
    }
}
