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
        public async Task<OpenDriveResult> GetFolderInfo(string folder_id, string sharing_id = null)
        {
            var query = new NameValueCollection();
            query.AddOptionalParameter(nameof(sharing_id), sharing_id);
            return await client.Get<FolderInfoResult>(ApiEndpoints.GetRequestUri(ApiEndpoints.Folder.Info(await GetSessionId().ConfigureAwait(false), folder_id), query)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> GetFolderItemByName(string folder_id, string name, string sharing_id = null, int? encryption_supported = null)
        {
            var query = new NameValueCollection();
            query.AddRequiredParameter(nameof(name), name);
            query.AddOptionalParameter(nameof(sharing_id), sharing_id);
            query.AddOptionalParameter(nameof(encryption_supported), encryption_supported);
            return await client.Get<FolderItemByNameResult>(ApiEndpoints.GetRequestUri(ApiEndpoints.Folder.ItemByName(await GetSessionId().ConfigureAwait(false), folder_id), query)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> GetFolderPath(string folder_id)
        {
            return await client.Get<FolderPathResult>(ApiEndpoints.Folder.Path(await GetSessionId().ConfigureAwait(false), folder_id)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> GetFolderIdByPath(string path)
        {
            return await client.Post<FolderIdByPathResult>(
                ApiEndpoints.Folder.IdByPath,
                JsonContent.Create(new FolderIdByPathInfo
                {
                    SessionId = await GetSessionId().ConfigureAwait(false),
                    Path = path
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> ListFolderContents(string folder_id, string search_query = null, int? last_request_time = null, string sharing_id = null, int? encryption_supported = null, bool? only_subfolders = null, bool? with_breadcrumbs = null, int? offset = null, string order_by = null, string order_type = null)
        {
            var query = new NameValueCollection();
            query.AddOptionalParameter(nameof(search_query), search_query);
            query.AddOptionalParameter(nameof(last_request_time), last_request_time);
            query.AddOptionalParameter(nameof(sharing_id), sharing_id);
            query.AddOptionalParameter(nameof(encryption_supported), encryption_supported);
            query.AddOptionalParameter(nameof(only_subfolders), only_subfolders);
            query.AddOptionalParameter(nameof(with_breadcrumbs), with_breadcrumbs);
            query.AddOptionalParameter(nameof(offset), offset);
            query.AddOptionalParameter(nameof(order_by), order_by);
            query.AddOptionalParameter(nameof(order_type), order_type);
            return await client.Get<ListFolderContentResult>(
                ApiEndpoints.GetRequestUri(ApiEndpoints.Folder.List(await GetSessionId().ConfigureAwait(false), folder_id), query)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> CreateFolder(string folder_name, string folder_sub_parent = null, int? folder_is_public  = null, int? folder_public_upl = null, int? folder_public_display = null, int? folder_public_dnl = null, int? folder_display_subfolders = null, string folder_description = null, string sharing_id = null)
        {
            return await client.Post<CreateFolderResult>(
                ApiEndpoints.Folder.Create,
                JsonContent.Create(new CreateFolderInfo
                {
                    SessionId = await GetSessionId().ConfigureAwait(false),
                    Name = folder_name,
                    SubParent = folder_sub_parent,
                    IsPublic = folder_is_public,
                    PublicUpload = folder_public_upl,
                    PublicDisplay = folder_public_display,
                    PublicDownload = folder_public_dnl,
                    DisplaySubfolders = folder_display_subfolders,
                    Description = folder_description,
                    SharingId = sharing_id
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> RenameFolder(string folder_id, string folder_name, string sharing_id = null)
        {
            return await client.Post<RenameFolderResult>(
                ApiEndpoints.Folder.Rename,
                JsonContent.Create(new RenameFolderInfo
                {
                    SessionId = await GetSessionId().ConfigureAwait(false),
                    FolderId = folder_id,
                    NewFolderName = folder_name,
                    SharingId = sharing_id
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> TrashFolder(IEnumerable<string> folder_ids, string sharing_id = null)
        {
            return await client.Post<TrashFolderResult>(
                ApiEndpoints.Folder.Trash,
                JsonContent.Create(new TrashFolderInfo
                {
                    SessionId = await GetSessionId().ConfigureAwait(false),
                    FolderId = string.Join(",", folder_ids),
                    SharingId = sharing_id
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }
    }

    public class FolderInfoResult : SuccessfulResult
    {
        public string FolderId { get; set; }
        public string Name { get; set; }
        public long DateCreated { get; set; }
        public long DateTrashed { get; set; }
        public long DirUpdateTime { get; set; }
        public int Access { get; set; }
        public bool PublicUpload { get; set; }
        public bool PublicContent { get; set; }
        public long DateModified { get; set; }
        public int Owner { get; set; }
        public bool DisplaySubfolders { get; set; }
        public string Shared { get; set; }
        public int ChildFolders { get; set; }
        public int OwnerLevel { get; set; }
        public bool OwnerSuspended { get; set; }
        public bool PublicDownload { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public int Permission { get; set; }
        public string Link { get; set; }
        public string Lang { get; set; }
        public int Encrypted { get; set; }
    }

    public class FolderItemByNameResult : SuccessfulResult
    {
        public long DirUpdateTime { get; set; }
        public Folder[] Folders { get; set; }
        public File[] Files { get; set; }

        public class Folder
        {
            public string FolderId { get; set; }
            public string Name { get; set; }
            public long DateCreated { get; set; }
            public long DirUpdateTime { get; set; }
            public int Access { get; set; }
            public long DateModified { get; set; }
            public string Shared { get; set; }
            public int Encrypted { get; set; }
        }

        public class File
        {
            public string FileId { get; set; }
            public string Name { get; set; }
            public int GroupId { get; set; }
            public string Extension { get; set; }
            public long Size { get; set; }
            public int Views { get; set; }
            public string Version { get; set; }
            public int Downloads { get; set; }
            public long DateModified { get; set; }
            public int Access { get; set; }
            public string Link { get; set; }
            public string DownloadLink { get; set; }
            public string StreamingLink { get; set; }
            public string TempStreamingLink { get; set; }
            public string ThumbLink { get; set; }
            public int Encrypted { get; set; }
            public string Password { get; set; }
        }
    }

    public class FolderPathResult : SuccessfulResult
    {
        public string Path { get; set; }
    }

    public class FolderIdByPathInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("path")]
        public string Path { get; set; }
    }

    public class FolderIdByPathResult : SuccessfulResult
    {
        public string FolderId { get; set; }
    }

    public class ListFolderContentResult : SuccessfulResult
    {
        public long DirUpdateTime { get; set; }
        public string Name { get; set; }
        public string ParentFolderId { get; set; }
        public string DirectFolderLink { get; set; }
        public int ResponseType { get; set; }
        public Folder[] Folders { get; set; }
        public File[] Files { get; set; }

        public class Folder
        {
            public string FolderId { get; set; }
            public string Name { get; set; }
            public long DateCreated { get; set; }
            public long DirUpdateTime { get; set; }
            public int Access { get; set; }
            public long DateModified { get; set; }
            public string Shared { get; set; }
            public int ChildFolders { get; set; }
            public string Link { get; set; }
            public int Encrypted { get; set; }
        }

        public class File
        {
            public string FileId { get; set; }
            public string Name { get; set; }
            public int GroupId { get; set; }
            public string Extension { get; set; }
            public long Size { get; set; }
            public int Views { get; set; }
            public string Version { get; set; }
            public int Downloads { get; set; }
            public long DateModified { get; set; }
            public int Access { get; set; }
            public string FileHash { get; set; }
            public string Link { get; set; }
            public string DownloadLink { get; set; }
            public string StreamingLink { get; set; }
            public string TempStreamingLink { get; set; }
            public string EditLink { get; set; }
            public string ThumbLink { get; set; }
            public int Encrypted { get; set; }
            public string Password { get; set; }
            public int EditOnline { get; set; }
        }

    }

    public class CreateFolderInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("folder_name")]
        public string Name { get; set; }
        [JsonPropertyName("folder_sub_parent")]
        public string SubParent { get; set; }
        [JsonPropertyName("folder_is_public")]
        public int? IsPublic { get; set; }
        [JsonPropertyName("folder_public_upl")]
        public int? PublicUpload { get; set; }
        [JsonPropertyName("folder_public_display")]
        public int? PublicDisplay { get; set; }
        [JsonPropertyName("folder_public_dnl")]
        public int? PublicDownload { get; set; }
        [JsonPropertyName("folder_display_subfolders")]
        public int? DisplaySubfolders { get; set; }
        [JsonPropertyName("folder_description")]
        public string Description { get; set; }
        [JsonPropertyName("sharing_id")]
        public string SharingId { get; set; }
    }

    public class CreateFolderResult : SuccessfulResult
    {
        public string FolderId { get; set; }
        public string Name { get; set; }
        public long DateCreated { get; set; }
        public long DateTrashed { get; set; }
        public int Access { get; set; }
        public long DateModified { get; set; }
        public string Shared { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }

    public class RenameFolderInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("folder_id")]
        public string FolderId { get; set; }
        [JsonPropertyName("folder_name")]
        public string NewFolderName { get; set; }
        [JsonPropertyName("sharing_id")]
        public string SharingId { get; set; }
    }

    public class RenameFolderResult : SuccessfulResult
    {
        public string FolderId { get; set; }
        public string Name { get; set; }
        public long DateCreated { get; set; }
        public long DirUpdateTime { get; set; }
        public int Access { get; set; }
        public long DateModified { get; set; }
        public string Shared { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }

    public class TrashFolderInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("folder_id")]
        public string FolderId { get; set; }
        [JsonPropertyName("sharing_id")]
        public string SharingId { get; set; }
    }

    public class TrashFolderResult : SuccessfulResult
    {
        public long DirUpdateTime { get; set; }
    }

}
