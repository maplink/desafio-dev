using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Maplink.DesafioDev.Infrastructure
{
    public class RestHandler
    {
        public virtual async Task<dynamic> Get(string url)
        {
            var request = new RestRequest("", Method.GET);
            var response = await GetContent(url, request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"failed to consume api rest with get action. url '{url}'.");
            }

            dynamic data = JsonConvert.DeserializeObject(response.Content);
            return data;
        }

        protected virtual async Task<IRestResponse> GetContent(string url, IRestRequest request)
        {
            var client = new RestClient(url);
            
            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();

            client.ExecuteAsync(request, response =>
            {
                taskCompletionSource.SetResult(response);
            });

            return await taskCompletionSource.Task;
        }
    }
}