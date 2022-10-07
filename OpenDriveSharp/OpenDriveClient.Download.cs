using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenDriveSharp
{
    public partial class OpenDriveClient
    {
        public async Task<OpenDriveResult> Download(string file_id, int? offset = null, bool? inline = null, string sharing_id = null, int? test = null, int? backup = null, string temp_key = null)
        {
            var query = new NameValueCollection()
                .AddRequiredParameter("session_id", await GetSessionId().ConfigureAwait(false))
                .AddOptionalParameter(nameof(offset), offset)
                .AddOptionalParameter(nameof(inline), inline)
                .AddOptionalParameter(nameof(sharing_id), sharing_id)
                .AddOptionalParameter(nameof(test), test)
                .AddOptionalParameter(nameof(backup), backup)
                .AddOptionalParameter(nameof(temp_key), temp_key);
            return await client.GetRaw<DownloadFileResult>(ApiEndpoints.GetRequestUri(ApiEndpoints.Download.File(file_id), query)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> DownloadAll(IEnumerable<string> files, IEnumerable<string> folders)
        {
            return await client.PostRaw<DownloadAllResult>(
                ApiEndpoints.Download.All,
                JsonContent.Create(new DownloadAllInfo
                {
                    SessionId = await GetSessionId().ConfigureAwait(false),
                    Files = string.Join(",", files),
                    Folders = string.Join(",", folders)
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }
    }

    public class DownloadFileResult : RawResult
    {

    }

    public class DownloadAllInfo
    {
        [JsonPropertyName("files")]
        public string Files { get; set; }
        [JsonPropertyName("folders")]
        public string Folders { get; set; }
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
    }

    public class DownloadAllResult : RawResult
    {

    }

}
