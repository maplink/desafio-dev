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
            var client = new RestClient(url);
            var request = new RestRequest("", Method.GET);

            var taskCompletionSource = new TaskCompletionSource<dynamic>();

            client.ExecuteAsync(request, response =>
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"failed to consume api rest with get action. url {url}.");
                }

                dynamic data = JsonConvert.DeserializeObject(response.Content);
                taskCompletionSource.SetResult(data);
            });

            return await taskCompletionSource.Task;
        }
    }
}