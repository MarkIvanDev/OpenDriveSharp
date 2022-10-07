using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenDriveSharp
{
    public partial class OpenDriveClient : IDisposable
    {
        private readonly string email;
        private readonly string password;
        private readonly HttpClient client;

        private string cachedSessionId;

        public OpenDriveClient(string email, string password) : this(email, password, new HttpClient())
        {
        }

        public OpenDriveClient(string email, string password, HttpClient client)
        {
            this.email = email;
            this.password = password;
            this.client = client;
            client.BaseAddress = new Uri(ApiEndpoints.Base);
        }

        public void SetSessionId(string sessionId)
        {
            cachedSessionId = sessionId;
        }

        public async Task<string> GetSessionId()
        {
            if (!string.IsNullOrEmpty(cachedSessionId))
            {
                var exists = await SessionExists(cachedSessionId).ConfigureAwait(false);
                if (exists.IsSuccessful && ((SessionExistsResult)exists).Result)
                {
                    return cachedSessionId;
                }
            }
            var login = await Login().ConfigureAwait(false);
            if (login.IsSuccessful && login is SessionLoginResult loginResult)
            {
                cachedSessionId = loginResult.SessionId;
                return cachedSessionId;
            }
            else
            {
                return string.Empty;
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }

    public abstract class OpenDriveResult
    {
        public OpenDriveResult(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }

        public bool IsSuccessful { get; }
    }

    public abstract class SuccessfulResult : OpenDriveResult
    {
        public SuccessfulResult() : base(true)
        {
        }
    }

    public class RawResult : SuccessfulResult
    {
        public byte[] Raw { get; set; }
    }

    public class ErrorResult : OpenDriveResult
    {
        public ErrorResult() : base(false)
        {
        }

        public ErrorData Error { get; set; }

        public class ErrorData
        {
            public int Code { get; set; }
            public string Message { get; set; }
        }
    }
}
