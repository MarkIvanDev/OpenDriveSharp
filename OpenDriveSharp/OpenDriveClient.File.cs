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
        public async Task<OpenDriveResult> GetFileInfo(string file_id, string sharing_id = null)
        {
            var query = new NameValueCollection();
            query.AddRequiredParameter("session_id", await GetSessionId().ConfigureAwait(false));
            query.AddOptionalParameter(nameof(sharing_id), sharing_id);
            return await client.Get<FileInfoResult>(ApiEndpoints.GetRequestUri(ApiEndpoints.File.Info(file_id), query)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> GetFilePath(string file_id)
        {
            return await client.Get<FilePathResult>(ApiEndpoints.File.Path(await GetSessionId().ConfigureAwait(false), file_id)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> GetFileThumb(string file_id, string sharing_id = null)
        {
            var query = new NameValueCollection();
            query.AddRequiredParameter("session_id", await GetSessionId().ConfigureAwait(false));
            query.AddOptionalParameter(nameof(sharing_id), sharing_id);
            return await client.GetRaw<FileThumbResult>(ApiEndpoints.GetRequestUri(ApiEndpoints.File.Thumb(file_id), query)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> GetFileIdByPath(string path)
        {
            return await client.Post<FileIdByPathResult>(
                ApiEndpoints.File.IdByPath,
                JsonContent.Create(new FileIdByPathInfo
                {
                    SessionId = await GetSessionId().ConfigureAwait(false),
                    Path = path
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> CreateFile(string access_folder_id, string folder_id, string file_type, string sharing_id = null)
        {
            return await client.Post<CreateFileResult>(
                ApiEndpoints.File.Create,
                JsonContent.Create(new CreateFileInfo
                {
                    SessionId = await GetSessionId().ConfigureAwait(false),
                    AccessFolderId = access_folder_id,
                    FolderId = folder_id,
                    FileType = file_type,
                    SharingId = sharing_id
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> RenameFile(string file_id, string new_file_name, string access_folder_id = null, string sharing_id = null)
        {
            return await client.Post<RenameFileResult>(
                ApiEndpoints.File.Rename,
                JsonContent.Create(new RenameFileInfo
                {
                    SessionId = await GetSessionId().ConfigureAwait(false),
                    FileId = file_id,
                    NewFileName = new_file_name,
                    AccessFolderId = access_folder_id,
                    SharingId = sharing_id
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> TrashFile(IEnumerable<string> file_ids, string access_folder_id = null, string sharing_id = null)
        {
            return await client.Post<TrashFileResult>(
                ApiEndpoints.File.Trash,
                JsonContent.Create(new TrashFileInfo
                {
                    SessionId = await GetSessionId().ConfigureAwait(false),
                    FileId = string.Join(",", file_ids),
                    AccessFolderId = access_folder_id,
                    SharingId = sharing_id
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }
    }

    public class FileInfoResult : SuccessfulResult
    {
        public string FileId { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public int Views { get; set; }
        public string Version { get; set; }
        public int Downloads { get; set; }
        public long DateTrashed { get; set; }
        public long DateModified { get; set; }
        public int Access { get; set; }
        public string FileHash { get; set; }
        public string Link { get; set; }
        public string DownloadLink { get; set; }
        public string StreamingLink { get; set; }
        public string TempStreamingLink { get; set; }
        public string OwnerName { get; set; }
        public int BWExceeded { get; set; }
        public int Encrypted { get; set; }
        public string Password { get; set; }
        public int EditOnline { get; set; }
        public string Description { get; set; }
        public int IsArchive { get; set; }
        public long Date { get; set; }
        public long DateUploaded { get; set; }
        public long DateAccessed { get; set; }
        public int AccessDisabled { get; set; }
        public string DestUrl { get; set; }
        public string Owner { get; set; }
        public string AccessUser { get; set; }
        public long DirUpdateTime { get; set; }
    }

    public class FilePathResult : SuccessfulResult
    {
        public string Path { get; set; }
    }

    public class FileThumbResult : RawResult
    {
        
    }

    public class FileIdByPathInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
    }

    public class FileIdByPathResult : SuccessfulResult
    {
        public string FileId { get; set; }
        public string DownloadLink { get; set; }
    }

    public class CreateFileInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("access_folder_id")]
        public string AccessFolderId { get; set; }
        [JsonPropertyName("folder_id")]
        public string FolderId { get; set; }
        [JsonPropertyName("file_type")]
        public string FileType { get; set; }
        [JsonPropertyName("sharing_id")]
        public string SharingId { get; set; }
    }

    public class CreateFileResult : SuccessfulResult
    {
        public string FileId { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public int Views { get; set; }
        public int Downloads { get; set; }
        public long DateModified { get; set; }
        public int Access { get; set; }
        public string Link { get; set; }
        public string DownloadLink { get; set; }
        public string StreamingLink { get; set; }
        public string TempStreamingLink { get; set; }
        public int Encrypted { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public long DirUpdateTime { get; set; }
    }

    public class RenameFileInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }
        [JsonPropertyName("new_file_name")]
        public string NewFileName { get; set; }
        [JsonPropertyName("access_folder_id")]
        public string AccessFolderId { get; set; }
        [JsonPropertyName("sharing_id")]
        public string SharingId { get; set; }
    }

    public class RenameFileResult : SuccessfulResult
    {
        public string FileId { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public int Views { get; set; }
        public int Downloads { get; set; }
        public long DateModified { get; set; }
        public int Access { get; set; }
        public string Link { get; set; }
        public string DownloadLink { get; set; }
        public string StreamingLink { get; set; }
        public string TempStreamingLink { get; set; }
        public int Encrypted { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public long DirUpdateTime { get; set; }
    }

    public class TrashFileInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }
        [JsonPropertyName("access_folder_id")]
        public string AccessFolderId { get; set; }
        [JsonPropertyName("sharing_id")]
        public string SharingId { get; set; }
    }

    public class TrashFileResult : SuccessfulResult
    {
        public long DirUpdateTime { get; set; }
    }

}
