using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenDriveSharp
{
    public partial class OpenDriveClient
    {
        public async Task<OpenDriveResult> CheckFileExistsByName(string folder_id, string[] names, string sharing_id = null)
        {
            return await client.Post<CheckFileExistsByNameResult>(
                ApiEndpoints.Upload.CheckFileExistsByName(folder_id),
                JsonContent.Create(new CheckFileExistsByNameInfo
                {
                    Name = names,
                    SessionId = await GetSessionId().ConfigureAwait(false),
                    SharingId = sharing_id
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> UploadFile(Stream stream, string folder_id, string file_name, int file_size, string file_hash, string file_description = null, string access_folder_id = null, string sharing_id = null, int? open_if_exists = null, int? file_time = null, int? file_compressed = null)
        {
            var result = await client.Post<UploadCreateFileResult>(
                ApiEndpoints.Upload.CreateFile,
                JsonContent.Create(new UploadCreateFileInfo
                {
                    SessionId = await GetSessionId().ConfigureAwait(false),
                    FolderId = folder_id,
                    FileName = file_name,
                    Description = file_description,
                    AccessFolderId = access_folder_id,
                    FileSize = file_size,
                    FileHash = file_hash,
                    SharingId = sharing_id,
                    OpenIfExists = open_if_exists
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
            if (result.IsSuccessful && result is UploadCreateFileResult createFileResult)
            {
                if (createFileResult.RequireHashOnly == 0 || createFileResult.Size != file_size)
                {
                    int offset = 0;
                    const int chunkSize = 1024 * 1024;
                    byte[] buffer = new byte[chunkSize];
                    stream.Position = 0;
                    var bytesRead = stream.Read(buffer, 0, chunkSize);
                    while (bytesRead > 0)
                    {
                        var fileData = buffer.Take(bytesRead).ToArray();
                        var byteArrayContent = new ByteArrayContent(fileData);
                        byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                        var content = new MultipartFormDataContent
                        {
                            { new StringContent(await GetSessionId()), "session_id" },
                            { new StringContent(createFileResult.FileId), "file_id"},
                            { new StringContent(createFileResult.TempLocation), "temp_location" },
                            { new StringContent(bytesRead.ToString()), "chunk_size" },
                            { new StringContent(offset.ToString()), "chunk_offset" },
                            { byteArrayContent, "file_data", file_name }
                        };
                        
                        await client.Post<UploadFileChunkResult>(ApiEndpoints.Upload.UploadFileChunk, content).ConfigureAwait(false);
                        
                        offset += bytesRead;
                        bytesRead = stream.Read(buffer, 0, chunkSize);
                    }
                }

                return await client.Post<UploadCloseFileResult>(
                    ApiEndpoints.Upload.CloseFileUpload,
                    JsonContent.Create(new UploadCloseFileInfo
                    {
                        SessionId = await GetSessionId(),
                        FileId = createFileResult.FileId,
                        FileSize = file_size,
                        FileHash = file_hash,
                        TempLocation = createFileResult.TempLocation,
                        FileTime = file_time,
                        FileCompressed = file_compressed,
                        AccessFolderId = access_folder_id,
                        SharingId = sharing_id
                    }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
            }
            else
            {
                return result;
            }
        }
    }

    public class CheckFileExistsByNameInfo
    {
        [JsonPropertyName("name")]
        public string[] Name { get; set; }
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("sharing_id")]
        public string SharingId { get; set; }
    }

    public class CheckFileExistsByNameResult : SuccessfulResult
    {
        public string[] Result { get; set; }
    }

    public class UploadCreateFileInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("folder_id")]
        public string FolderId { get; set; }
        [JsonPropertyName("file_name")]
        public string FileName { get; set; }
        [JsonPropertyName("file_description")]
        public string Description { get; set; }
        [JsonPropertyName("access_folder_id")]
        public string AccessFolderId { get; set; }
        [JsonPropertyName("file_size")]
        public int? FileSize { get; set; }
        [JsonPropertyName("file_hash")]
        public string FileHash { get; set; }
        [JsonPropertyName("sharing_id")]
        public string SharingId { get; set; }
        [JsonPropertyName("open_if_exists")]
        public int? OpenIfExists { get; set; }
    }

    public class UploadCreateFileResult : SuccessfulResult
    {
        public string FileId { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public int Views { get; set; }
        public string Version { get; set; }
        public int Downloads { get; set; }
        public int Access { get; set; }
        public string Link { get; set; }
        public string DownloadLink { get; set; }
        public string StreamingLink { get; set; }
        public long DirUpdateTime { get; set; }
        public string TempLocation { get; set; }
        public int SpeedLimit { get; set; }
        public int RequireCompression { get; set; }
        public int RequireHash { get; set; }
        public int RequireHashOnly { get; set; }
    }

    public class UploadFileChunkResult : SuccessfulResult
    {
        public int TotalWritten { get; set; }
    }

    public class UploadCloseFileInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("file_id")]
        public string FileId { get; set; }
        [JsonPropertyName("file_size")]
        public int FileSize { get; set; }
        [JsonPropertyName("temp_location")]
        public string TempLocation { get; set; }
        [JsonPropertyName("file_time")]
        public int? FileTime { get; set; }
        [JsonPropertyName("access_folder_id")]
        public string AccessFolderId { get; set; }
        [JsonPropertyName("file_compressed")]
        public int? FileCompressed { get; set; }
        [JsonPropertyName("file_hash")]
        public string FileHash { get; set; }
        [JsonPropertyName("sharing_id")]
        public string SharingId { get; set; }
    }

    public class UploadCloseFileResult : SuccessfulResult
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
        public bool OwnerSuspended { get; set; }
        public string AccType { get; set; }
        public string FileHash { get; set; }
        public string Link { get; set; }
        public string DownloadLink { get; set; }
        public string StreamingLink { get; set; }
        public string OwnerName { get; set; }
        [JsonPropertyName("upload_speed_limit")]
        public int UploadSpeedLimit { get; set; }
        [JsonPropertyName("download_speed_limit")]
        public int DownloadSpeedLimit { get; set; }
        public int BWExceeded { get; set; }
        public string ThumbLink { get; set; }
        public int Encrypted { get; set; }
        public string Password { get; set; }
        public string OwnerLevel { get; set; }
        public int EditOnline { get; set; }
        public string Id { get; set; }
        public string FolderId { get; set; }
        public string Description { get; set; }
        public int IsArchive { get; set; }
        public string Category { get; set; }
        public long Date { get; set; }
        public long DateUploaded { get; set; }
        public long DateAccessed { get; set; }
        public string DirectLinkPublic { get; set; }
        public string EmbedLink { get; set; }
        public int AccessDisabled { get; set; }
        public string Owner { get; set; }
        public string AccessUser { get; set; }
        public long DirUpdateTime { get; set; }
        public string FileName { get; set; }
        public string FileDate { get; set; }
        public string FileDescription { get; set; }
        public string FileKey { get; set; }
        public string FilePrice { get; set; }
        public string FileVersion { get; set; }
        public string FileIp { get; set; }
        public string FileIsPublic { get; set; }
        public string DateTime { get; set; }
    }

}
