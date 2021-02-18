using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CartService.DataAccess.WebClient
{
    public class HttpCalls
    {
        HttpClient _client;
        public HttpCalls()
        {
            _client = new HttpClient();
        }
        public async Task<(TResponse Response, HttpResponseMessage ResponseMessage)> SendRequest<TRequestInput, TResponse>(HttpMethod methodType, Uri uri, TRequestInput requestInput)
        {
            try
            {
                HttpResponseMessage ResponseMessage;
                ResponseMessage = await RetrieveResponse(methodType, uri, requestInput).ConfigureAwait(false);
                var responseData = await ResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);



                if (ResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    if (!string.IsNullOrWhiteSpace(responseData))
                    {
                        return (Response: JsonConvert.DeserializeObject<TResponse>(responseData), ResponseMessage: ResponseMessage);
                    }
                    else
                    {
                        return (Response: default(TResponse), ResponseMessage: ResponseMessage);
                    }
                }
                return (Response: default(TResponse), ResponseMessage: ResponseMessage);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<HttpResponseMessage> RetrieveResponse<TRequestInput>(HttpMethod methodType, Uri uri, TRequestInput requestInput)
        {
            var requestMessage = CreateCommonRequestMessage(methodType, uri);
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestInput), Encoding.UTF8, "application/json");
            return await _client.SendAsync(requestMessage).ConfigureAwait(false);
        }

        public async Task<TResponse> GetClient<TRequest, TResponse>(TRequest input, Uri uri)
        {
            (TResponse Response, HttpResponseMessage ResponseMessage) Result = await SendRequest<TRequest, TResponse>(HttpMethod.Get, uri, input).ConfigureAwait(false);
            Result.ResponseMessage.EnsureSuccessStatusCode();
            return Result.Response;
        }

        private HttpRequestMessage CreateCommonRequestMessage(HttpMethod methodType, Uri uri)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();

            requestMessage.Method = methodType;
            requestMessage.RequestUri = uri;
            return requestMessage;
        }
    }
}
