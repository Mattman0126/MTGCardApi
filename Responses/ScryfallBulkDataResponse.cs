using Newtonsoft.Json;

namespace MTGCardApi.Responses;

public class ScryfallBulkDataResponse
{
    [JsonProperty("download_uri")]
    public string DownloadUri { get; set; } = null!;
}
