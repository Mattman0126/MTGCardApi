using Newtonsoft.Json;

namespace MTGCardApi.Dtos.Responses;

public class ScryfallBulkDataResponse
{
    [JsonProperty("download_uri")]
    public string DownloadUri { get; set; } = null!;
}
