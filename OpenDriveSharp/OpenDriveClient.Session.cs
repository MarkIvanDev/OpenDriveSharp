using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenDriveSharp
{
    public partial class OpenDriveClient
    {
        public async Task<OpenDriveResult> GetSessionInfo(string session_id)
        {
            return await client.Get<SessionInfoResult>(ApiEndpoints.Session.Info(session_id)).ConfigureAwait(false);
        }
        
        public async Task<OpenDriveResult> SessionExists(string session_id)
        {
            return await client.Post<SessionExistsResult>(
                ApiEndpoints.Session.Exists,
                JsonContent.Create(new SessionExistsInfo
                {
                    SessionId = session_id
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> Login(string version = null, string partner_id = null)
        {
            return await client.Post<SessionLoginResult>(
                ApiEndpoints.Session.Login,
                JsonContent.Create(new SessionLoginInfo
                {
                    Username = email,
                    Password = password,
                    Version = version,
                    PartnerId = partner_id
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }

        public async Task<OpenDriveResult> Logout(string session_id)
        {
            return await client.Post<SessionLogoutResult>(
                ApiEndpoints.Session.Logout,
                JsonContent.Create(new SessionLogoutInfo
                {
                    SessionId = session_id
                }, options: ApiExtensions.JSON_OPTIONS)).ConfigureAwait(false);
        }

    }

    public class SessionInfoResult : SuccessfulResult
    {
        public string SessionID { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string AccType { get; set; }
        public string UserLang { get; set; }
        public string Enable2FA { get; set; }
        public string Active2FA { get; set; }
        public string UserID { get; set; }
        public string IsAccountUser { get; set; }
        public string DriveName { get; set; }
        public string UserLevel { get; set; }
        public string UserPlan { get; set; }
        public string FVersioning { get; set; }
        public string UserDomain { get; set; }
        public string PartnerUsersDomain { get; set; }
        public int UploadSpeedLimit { get; set; }
        public int DownloadSpeedLimit { get; set; }
        public int UploadsPerSecond { get; set; }
        public int DownloadPerSecond { get; set; }
        public string Encoding { get; set; }
        public int IsPartner { get; set; }
    }

    public class SessionExistsInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
    }

    public class SessionExistsResult : SuccessfulResult
    {
        public bool Result { get; set; }
    }

    public class SessionLoginInfo
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("passwd")]
        public string Password { get; set; }
        public string Version { get; set; }
        [JsonPropertyName("partner_id")]
        public string PartnerId { get; set; }
    }

    public class SessionLoginResult : SuccessfulResult
    {
        public string SessionId { get; set; }
        public string Username { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int AccType { get; set; }
        public string UserLang { get; set; }
        public int IsAccessUser { get; set; }
        public string DriveName { get; set; }
        public string UserLevel { get; set; }
        public string UserPlan { get; set; }
        public string FVersioning { get; set; }
        public string UserDomain { get; set; }
        public string PartnerUsersDomain { get; set; }
        public string IsPartner { get; set; }
        public string Encoding { get; set; }
    }

    public  class SessionLogoutInfo
    {
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
    }

    public  class SessionLogoutResult : SuccessfulResult
    {
        public bool Result { get; set; }
    }

}
