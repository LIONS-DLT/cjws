using System.Net.Http.Json;
using System.Text.Json;

namespace DataExchangeClient
{
    public class DataExchangeService
    {
        public string BaseURL { get; set; }
        public string ApiKey { get; set; }

        private static JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public DataExchangeService(string baseUrl, string apiKey)
        {
            this.BaseURL = baseUrl;
            this.ApiKey = apiKey;    
        }

        public DataExchangeResult RegisterMessage()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                return httpClient.GetAsync(string.Format("{0}/Register?key={1}", this.BaseURL, this.ApiKey))
                    .Result.Content.ReadFromJsonAsync<DataExchangeResult>(serializerOptions).Result!;
            }
        }

        public DataExchangeResult UploadMessage(byte[] bytes)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ByteArrayContent content = new ByteArrayContent(bytes);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                return httpClient.PostAsync(string.Format("{0}/Upload?key={1}", this.BaseURL, this.ApiKey), content)
                    .Result.Content.ReadFromJsonAsync<DataExchangeResult>(serializerOptions).Result!;
            }
        }

        public static DataExchangeResult UploadMessage(string uploadUrl, byte[] bytes)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                ByteArrayContent content = new ByteArrayContent(bytes);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                return httpClient.PostAsync(uploadUrl, content)
                    .Result.Content.ReadFromJsonAsync<DataExchangeResult>(serializerOptions).Result!;
            }
        }

        public static byte[] RetrieveMessage(string retrieveUrl)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                return httpClient.GetAsync(retrieveUrl)
                    .Result.Content.ReadAsByteArrayAsync().Result;
            }
        }

        public DataExchangeResult RegisterMessageWithCallback(Action<byte[]?> callback)
        {
            DataExchangeResult result = this.RegisterMessage();
            string url = result.RetrieveUrl;

            DateTime start = DateTime.Now;

            new Thread(new ThreadStart(() =>
            {
                byte[]? data = null;
                while ((DateTime.Now - start).TotalSeconds < 60)
                {
                    try
                    {
                        data = RetrieveMessage(url);
                        if (data != null && data.Length > 0)
                            break;
                    }
                    catch
                    {
                        Thread.Sleep(1000);
                    }
                }

                callback(data);
            })).Start();

            return result;
        }
    }

    public class DataExchangeResult
    {
        public string RetrieveUrl { get; set; } = string.Empty;
        public string UploadUrl { get; set; } = string.Empty;
    }
}