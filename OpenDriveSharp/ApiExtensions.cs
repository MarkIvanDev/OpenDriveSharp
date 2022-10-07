using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenDriveSharp
{
    public static class ApiExtensions
    {
        public static readonly JsonSerializerOptions JSON_OPTIONS;

        static ApiExtensions()
        {
            JSON_OPTIONS = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
        }

        public static async Task<OpenDriveResult> Get<T>(this HttpClient client, string requestUri) where T : SuccessfulResult, new()
        {
            try
            {
                var response = await client.GetAsync(requestUri).ConfigureAwait(false);
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<T>(body, JSON_OPTIONS);
                }
                else
                {
                    return JsonSerializer.Deserialize<ErrorResult>(body, JSON_OPTIONS);
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult()
                {
                    Error = new ErrorResult.ErrorData
                    {
                        Message = ex.Message
                    }
                };
            }
        }

        public static async Task<OpenDriveResult> GetRaw<T>(this HttpClient client, string requestUri) where T : RawResult, new()
        {
            try
            {
                var response = await client.GetAsync(requestUri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    return new T()
                    {
                        Raw = body
                    };
                }
                else
                {
                    var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<ErrorResult>(body, JSON_OPTIONS);
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult()
                {
                    Error = new ErrorResult.ErrorData
                    {
                        Message = ex.Message
                    }
                };
            }
        }

        public static async Task<OpenDriveResult> Post<T>(this HttpClient client, string requestUri, HttpContent content) where T : SuccessfulResult, new()
        {
            try
            {
                var response = await client.PostAsync(requestUri, content).ConfigureAwait(false);
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<T>(body, JSON_OPTIONS);
                }
                else
                {
                    return JsonSerializer.Deserialize<ErrorResult>(body, JSON_OPTIONS);
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult()
                {
                    Error = new ErrorResult.ErrorData
                    {
                        Message = ex.Message
                    }
                };
            }
        }

        public static async Task<OpenDriveResult> PostRaw<T>(this HttpClient client, string requestUri, HttpContent content) where T : RawResult, new()
        {
            try
            {
                var response = await client.PostAsync(requestUri, content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    return new T()
                    {
                        Raw = body
                    };
                }
                else
                {
                    var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return JsonSerializer.Deserialize<ErrorResult>(body, JSON_OPTIONS);
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult()
                {
                    Error = new ErrorResult.ErrorData
                    {
                        Message = ex.Message
                    }
                };
            }
        }

        public static async Task<OpenDriveResult> Put<T>(this HttpClient client, string requestUri, HttpContent content) where T : SuccessfulResult, new()
        {
            try
            {
                var response = await client.PutAsync(requestUri, content).ConfigureAwait(false);
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<T>(body, JSON_OPTIONS);
                }
                else
                {
                    return JsonSerializer.Deserialize<ErrorResult>(body, JSON_OPTIONS);
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult()
                {
                    Error = new ErrorResult.ErrorData
                    {
                        Message = ex.Message
                    }
                };
            }
        }

        public static async Task<OpenDriveResult> Delete<T>(this HttpClient client, string requestUri) where T : SuccessfulResult, new()
        {
            try
            {
                var response = await client.DeleteAsync(requestUri).ConfigureAwait(false);
                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<T>(body, JSON_OPTIONS);
                }
                else
                {
                    return JsonSerializer.Deserialize<ErrorResult>(body, JSON_OPTIONS);
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult()
                {
                    Error = new ErrorResult.ErrorData
                    {
                        Message = ex.Message
                    }
                };
            }
        }

        public static NameValueCollection AddRequiredParameter(this NameValueCollection query, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                query.Add(key, value);
                return query;
            }
            else
            {
                throw new ArgumentNullException(nameof(value));
            }
        }

        public static NameValueCollection AddRequiredParameter<T>(this NameValueCollection query, string key, T value) where T : struct
        {
            query.Add(key, value.ToString());
            return query;
        }

        public static NameValueCollection AddOptionalParameter(this NameValueCollection query, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                query.Add(key, value);
            }
            return query;
        }

        public static NameValueCollection AddOptionalParameter<T>(this NameValueCollection query, string key, T? value) where T : struct
        {
            if (value.HasValue)
            {
                if (value.Value is bool b)
                {
                    query.Add(key, b ? "true" : "false");
                }
                else
                {
                    query.Add(key, value.Value.ToString());
                }
            }
            return query;
        }

    }
}
