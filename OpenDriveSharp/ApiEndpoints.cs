using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;

namespace OpenDriveSharp
{
    public static class ApiEndpoints
    {
        public const string Base = "https://dev.opendrive.com/api/";

        public static string GetRequestUri(string endpoint, NameValueCollection query)
        {
            var queryString = query.Count > 0 ?
                from key in query.AllKeys
                from value in query.GetValues(key)
                select string.Format("{0}={1}", WebUtility.UrlEncode(key), WebUtility.UrlEncode(value)) :
                Enumerable.Empty<string>();
            return queryString.Any() ? $"{endpoint}?{string.Join("&", queryString)}" : endpoint;
        }

        public static class AccountUsers
        {
            public static string FoldersList(string sessionId, string folderId) => $"v1/accountusers/folderslist.json/{sessionId}/{folderId}";
            public static string Info(string sessionId, string accessEmail) => $"v1/accountusers/info.json/{sessionId}/{accessEmail}";
            public static string SearchUsers(string sessionId) => $"v1/accountusers/searchusers.json/{sessionId}";
            public static string UsersInGroup(string sessionId, string groupId) => $"v1/accountusers/usersingroup.json/{sessionId}/{groupId}";
            public const string Create = "v1/accountusers.json";
            public const string Move = "v1/accountusers/move.json";
            public const string SetAccess = "v1/accountusers/setaccess.json";
            public const string SetFolderAccess = "v1/accountusers/setfolderaccess.json";
            public const string Update = "v1/accountusers.json";
            public static string Delete(string sessionId, string accessEmail) => $"v1/accountusers.json/{sessionId}/{accessEmail}";
        }

        public static class Branding
        {
            public const string Info = "v1/branding.json";
            public static string CheckUserSubdomainExists(string sessionId, string subdomain) => $"v1/branding/checkusersubdomainexists.json/{sessionId}/{subdomain}";
            public const string Files = "v1/branding/files.json";
            public static string Subdomain(string partnerName, string subdomain) => $"v1/branding/subdomainbranding.json/{partnerName}/{subdomain}";
            public const string Create = "v1/branding.json";
            public const string Update = "v1/branding.json";
            public static string Delete(string sessionId) => $"v1/branding.json/{sessionId}";
            public const string DeleteUserImage = "v1/branding/userimage.json";
        }

        public static class Download
        {
            public static string File(string fileId) => $"v1/download/file.json/{fileId}";
            public const string All = "v1/download/all.json";
        }

        public static class File
        {
            public static string ExpiringLink(string sessionId, string date, int counter, string fileId, string enable) => $"v1/file/expiringlink.json/{sessionId}/{date}/{counter}/{fileId}/{enable}";
            public static string FileExpiringLinks(string sessionId, string fileId) => $"v1/file/fileexpiringlinks.json/{sessionId}/{fileId}";
            public static string FileVersions(string sessionId, int fileGroupId) => $"v1/file/fileversions.json/{sessionId}/{fileGroupId}";
            public static string Info(string fileId) => $"v1/file/info.json/{fileId}";
            public static string Path(string sessionId, string fileId) => $"v1/file/path.json/{sessionId}/{fileId}";
            public static string Thumb(string fileId) => $"v1/file/thumb.json/{fileId}";
            public const string Create = "v1/file.json";
            public const string Access = "v1/file/access.json";
            public const string IdByPath = "v1/file/idbypath.json";
            public const string MoveCopy = "v1/file/move_copy.json";
            public const string Rename = "v1/file/rename.json";
            public const string Restore = "v1/file/restore.json";
            public const string Trash = "v1/file/trash.json";
            public const string VerifyPassword = "v1/file/verifypassword.json";
            public const string FileSettings = "v1/file/filesettings.json";
            public static string Delete(string sessionId, string fileId) => $"v1/file.json/{sessionId}/{fileId}";
            public static string RemoveFileVersion(string sessionId, string fileId) => $"v1/file/removefileversion.json/{sessionId}/{fileId}";
        }

        public static class Folder
        {
            public static string Breadcrumb(string sessionId, string folderId) => $"v1/folder/breadcrumb.json/{sessionId}/{folderId}";
            public static string ExpiringLink(string sessionId, string date, int counter, string folderId, string enable) => $"v1/folder/expiringlink.json/{sessionId}/{date}/{counter}/{folderId}/{enable}";
            public static string FolderExpiringLinks(string sessionId, string folderId) => $"v1/folder/folderexpiringlinks.json/{sessionId}/{folderId}";
            public static string Info(string sessionId, string folderId) => $"v1/folder/info.json/{sessionId}/{folderId}";
            public static string ItemByName(string sessionId, string folderId) => $"v1/folder/itembyname.json/{sessionId}/{folderId}";
            public static string List(string sessionId, string folderId) => $"v1/folder/list.json/{sessionId}/{folderId}";
            public static string Path(string sessionId, string folderId) => $"v1/folder/path.json/{sessionId}/{folderId}";
            public static string Shared(string folderId) => $"v1/folder/shared.json/{folderId}";
            public static string SharedInfo(string folderId) => $"v1/folder/sharedinfo.json/{folderId}";
            public static string Trashlist(string sessionId) => $"v1/folder/trashlist.json/{sessionId}";
            public static string UserAccessMode(string sessionId, string folderId) => $"v1/folder/useraccessmode.json/{sessionId}/{folderId}";
            public const string Create = "v1/folder.json";
            public const string IdByPath = "v1/folder/idbypath.json";
            public const string MoveCopy = "v1/folder/move_copy.json";
            public const string Remove = "v1/folder/remove.json";
            public const string Rename = "v1/folder/rename.json";
            public const string Restore = "v1/folder/restore.json";
            public const string SetAccess = "v1/folder/setaccess.json";
            public const string Trash = "v1/folder/trash.json";
            public const string FolderSettings = "v1/folder/foldersettings.json";
            public static string EmptyTrash(string sessionId) => $"v1/folder/trash.json/{sessionId}";
        }

        public static class Notes
        {
            public static string All(string sessionId, string itemType, string itemId) => $"v1/notes.json/{sessionId}/{itemType}/{itemId}";
            public static string Archived(string sessionId) => $"v1/notes/archived.json/{sessionId}";
            public static string NoteLists(string sessionId, string notepadId) => $"v1/notes/notelists.json/{sessionId}/{notepadId}";
            public static string Notepads(string sessionId) => $"v1/notes/notepads.json/{sessionId}";
            public static string Search(string sessionId) => $"v1/notes/search.json/{sessionId}";
            public static string Trashed(string sessionId) => $"v1/notes/trashed.json/{sessionId}";
            public const string CreateNote = "v1/notes.json";
            public const string AttachFile = "v1/notes/attachfile.json";
            public const string CreateNoteList = "v1/notes/notelist.json";
            public const string CreateNotepad = "v1/notes/notepad.json";
            public const string UpdateNote = "v1/notes.json";
            public static string ArchiveNoteList(string sessionId, string listId) => $"v1/notes/archivenotelist.json/{sessionId}/{listId}";
            public static string ArchiveNotepad(string sessionId, string notepadId) => $"v1/notes/archivenotepad.json/{sessionId}/{notepadId}";
            public static string UpdateNoteList(string sessionId, string listId) => $"v1/notes/notelist.json/{sessionId}/{listId}";
            public static string UpdateNotepad(string sessionId, string notepadId) => $"v1/notes/notepad.json/{sessionId}/{notepadId}";
            public static string OrderNoteLists(string sessionId) => $"v1/notes/ordernotelists.json/{sessionId}";
            public static string RestoreArchivedNoteList(string sessionId, string listId) => $"v1/notes/restorearchivednotelist.json/{sessionId}/{listId}";
            public static string RestoreArchivedNotepad(string sessionId, string notepadId) => $"v1/notes/restorearchivednotepad.json/{sessionId}/{notepadId}";
            public static string RestoreTrashedNoteList(string sessionId, string listId) => $"v1/notes/restoretrashednotelist.json/{sessionId}/{listId}";
            public static string RestoreTrashedNotepad(string sessionId, string notepadId) => $"v1/notes/restoretrashednotepad.json/{sessionId}/{notepadId}";
            public static string DeleteNote(string sessionId, string noteId) => $"v1/notes.json/{sessionId}/{noteId}";
            public static string DeleteNoteList(string sessionId, string listId) => $"v1/notes/notelist.json/{sessionId}/{listId}";
            public static string DeleteNotepad(string sessionId, string notepadId) => $"v1/notes/notepad.json/{sessionId}/{notepadId}";
            public static string EmptyTrash(string sessionId) => $"v1/notes/trash.json/{sessionId}";
            public static string TrashNoteList(string sessionId, string listId) => $"v1/notes/trashnotelist.json/{sessionId}/{listId}";
            public static string TrashNotepad(string sessionId, string notepadId) => $"v1/notes/trashnotepad.json/{sessionId}/{notepadId}";
        }

        public static class SecureFolders
        {
            public static string Auth(string sessionId, string folderId) => $"v1/securefolders/auth.json/{sessionId}/{folderId}";
            public const string AuthByHash = "v1/securefolders/auth.json";
            public const string Init = "v1/securefolders/init.json";
        }

        public static class Session
        {
            public static string Info(string sessionId) => $"v1/session/info.json/{sessionId}";
            public const string Exists = "v1/session/exists.json";
            public const string Login = "v1/session/login.json";
            public const string Logout = "v1/session/logout.json";
        }

        public static class Sharing
        {
            public static string ListSharedFolders(string sessionId, string sharingId) => $"v1/sharing/listsharedfolders.json/{sessionId}/{sharingId}";
            public static string ListSharedUsers(string sessionId, string noteId) => $"v1/sharing/listsharedusers.json/{sessionId}";
            public static string ListUsers(string sessionId, string folderId) => $"v1/sharing/listusers.json/{sessionId}/{folderId}";
            public const string Share = "v1/sharing.json";
            public const string SetMode = "v1/sharing/setmode.json";
            public static string Delete(string sessionId, string sharingId) => $"v1/sharing.json/{sessionId}/{sharingId}";
        }

        public static class Stats
        {
            public static string Bandwidth(string sessionId, int month, int year) => $"v1/stats/bandwidth.json/{sessionId}/{month}/{year}";
        }

        public static class Tasks
        {
            public const string ActiveTasks = "v1/tasks.json";
            public static string ArchivedItems(string sessionId, string itemType) => $"v1/tasks/archiveditems.json/{sessionId}/{itemType}";
            public static string ArchiveList(string sessionId) => $"v1/tasks/archivelist.json/{sessionId}";
            public const string ProjectLists = "v1/tasks/projectlists.json";
            public const string Projects = "v1/tasks/projects.json";
            public static string Search(string sessionId) => $"v1/tasks/search.json/{sessionId}";
            public static string Tags(string sessionId) => $"v1/tasks/tags.json/{sessionId}";
            public static string TaskComments(string sessionId, string taskId, int commentId) => $"v1/tasks/taskcomments.json/{sessionId}/{taskId}/{commentId}";
            public const string TaskLists = "v1/tasks/tasklists.json";
            public static string TrashedItems(string sessionId, string itemType) => $"v1/tasks/trasheditems.json/{sessionId}/{itemType}";
            public static string TrashList(string sessionId) => $"v1/tasks/trashlist.json/{sessionId}";
            public static string Users(string sessionId) => $"v1/tasks/users.json/{sessionId}";
            public static string CreateTask(string sessionId, string taskListId) => $"v1/tasks.json/{sessionId}/{taskListId}";
            public static string AddProjectPermission(string sessionId, string projectId) => $"v1/tasks/addprojectpermission.json/{sessionId}/{projectId}";
            public static string CreateComment(string sessionId, string taskId) => $"v1/tasks/comment.json/{sessionId}/{taskId}";
            public static string CreateProject(string sessionId, string projectListId) => $"v1/tasks/project.json/{sessionId}/{projectListId}";
            public static string CreateProjectList(string sessionId) => $"v1/tasks/projectlist.json/{sessionId}";
            public static string CreateSubcomment(string sessionId, int commentId) => $"v1/tasks/subcomment.json/{sessionId}/{commentId}";
            public static string CreateTag(string sessionId) => $"v1/tasks/tag.json/{sessionId}";
            public static string CreateTaskList(string sessionId, string projectId) => $"v1/tasks/tasklist.json/{sessionId}/{projectId}";
            public static string UpdateTask(string sessionId, string taskId) => $"v1/tasks.json/{sessionId}/{taskId}";
            public static string ArchiveTask(string sessionId, string taskId) => $"v1/tasks/archive.json/{sessionId}/{taskId}";
            public static string ArchiveProject(string sessionId, string projectId) => $"v1/tasks/archiveproject.json/{sessionId}/{projectId}";
            public static string ArchiveProjectList(string sessionId, string listId) => $"v1/tasks/archiveprojectlist.json/{sessionId}/{listId}";
            public static string ArchiveTaskList(string sessionId, string listId) => $"v1/tasks/archivetasklist.json/{sessionId}/{listId}";
            public static string UpdateComment(string sessionId, string commentId) => $"v1/tasks/comment.json/{sessionId}/{commentId}";
            public static string Move(string sessionId) => $"v1/tasks/move.json/{sessionId}";
            public static string UpdateProject(string sessionId, string projectId) => $"v1/tasks/project.json/{sessionId}/{projectId}";
            public static string UpdateProjectList(string sessionId, string listId) => $"v1/tasks/projectlist.json/{sessionId}/{listId}";
            public static string RestoreTask(string sessionId, string taskId) => $"v1/tasks/restore.json/{sessionId}/{taskId}";
            public static string RestoreTaskList(string sessionId, string listId) => $"v1/tasks/restoretasklist.json/{sessionId}/{listId}";
            public static string UpdateSubcomment(string sessionId, int subcommentId) => $"v1/tasks/subcomment.json/{sessionId}/{subcommentId}";
            public static string UpdateTag(string sessionId, int tagId) => $"v1/tasks/tag.json/{sessionId}/{tagId}";
            public static string UpdateTaskList(string sessionId, string listId) => $"v1/tasks/tasklist.json/{sessionId}/{listId}";
            public static string DeleteAttachedFile(string sessionId, string itemType, string itemId, string fileId) => $"v1/tasks/attachedfile.json/{sessionId}/{itemType}/{itemId}/{fileId}";
            public static string EmptyTrash(string sessionId) => $"v1/tasks/emptytrash.json/{sessionId}";
            public static string DeleteTag(string sessionId, int tagId) => $"v1/tasks/tag.json/{sessionId}/{tagId}";
            public static string TrashTask(string sessionId, string taskId) => $"v1/tasks/trash.json/{sessionId}/{taskId}";
            public static string TrashComment(string sessionId, int commentId) => $"v1/tasks/trashcomment.json/{sessionId}/{commentId}";
            public static string TrashProject(string sessionId, string projectId) => $"v1/tasks/trashproject.json/{sessionId}/{projectId}";
            public static string TrashProjectList(string sessionId, string listId) => $"v1/tasks/trashprojectlist.json/{sessionId}/{listId}";
            public static string TrashSubcomment(string sessionId, int subcommentId) => $"v1/tasks/trashsubcomment.json/{sessionId}/{subcommentId}";
            public static string TrashTaskList(string sessionId, string listId) => $"v1/tasks/archivelist.json/{sessionId}/{listId}";
        }

        public static class Upload
        {
            public static string CheckFileExistsByName(string folderId) => $"v1/upload/checkfileexistsbyname.json/{folderId}";
            public const string CloseFileUpload = "v1/upload/close_file_upload.json";
            public const string CreateFile = "v1/upload/create_file.json";
            public const string OpenFileUpload = "v1/upload/open_file_upload.json";
            public const string UploadFileChunk = "v1/upload/upload_file_chunk.json";
            public static string UploadFileChunk2(string sessionId, string fileId) => $"v1/upload/upload_file_chunk2.json/{sessionId}/{fileId}";
        }

        public static class Users
        {
            public static string Info(string sessionId) => $"v1/users/info.json/{sessionId}";
            public const string AffiliateSetting = "v1/users/affiliatesetting.json";
            public const string ConfirmPasswordReset = "v1/users/confirmpasswordreset.json";
            public const string ForgotPassword = "v1/users/forgotpassword.json";
            public const string VerifyEmail = "v1/users/verifyemail.json";
            public const string VerifyEmailSignup = "v1/users/verifyemailsignup.json";
            public const string UpdateEmail = "v1/users/email.json";
            public const string UpdateInfo = "v1/users/info.json";
            public const string UpdatePassword = "v1/users/password.json";
            public const string UpdateUsername = "v1/users/username.json";
        }

        public static class UserGroups
        {
            public static string Info(string sessionId, int groupId) => $"v1/usergroups.json/{sessionId}/{groupId}";
            public const string All = "v1/usergroups/all.json";
            public const string Create = "v1/usergroups.json";
            public const string Update = "v1/usergroups.json";
            public static string Delete(string sessionId, int groupId) => $"v1/usergroups.json/{sessionId}/{groupId}";
        }

        public static class OAuth2
        {
            public const string Grant = "v1/oauth2/grant.json";
        }
    }
}
