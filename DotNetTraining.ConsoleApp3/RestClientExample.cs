using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTraining.ConsoleApp3
{
    public class RestClientExample
    {
        private readonly RestClient _restClient;
        private readonly string url = "https://jsonplaceholder.typicode.com/posts";

        public RestClientExample()
        {
            _restClient = new RestClient();
        }

        public async Task Get()
        {
            RestRequest restClient = new RestRequest(url, Method.Get);
            var response = await _restClient.ExecuteAsync(restClient);

            if (response.IsSuccessStatusCode!)
            {
                Console.WriteLine("No data found");
            }
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                Console.WriteLine(content);
            }
        }

        public async Task Edit(int id)
        {
            RestRequest restRequest = new RestRequest($"{url}/{id}", Method.Get);
            var response = await _restClient.ExecuteAsync(restRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found");
            }

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content;
                Console.WriteLine(content);
            }

        }

        public async Task Post(string title, string body, int userId)
        {
            PostBody postBody = new PostBody
            {
                title = title,
                body = body,
                userId = userId
            };

            RestRequest restRequest = new RestRequest(url, Method.Post);
            restRequest.AddJsonBody(postBody);

            var response = await _restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.Content!);
            }
        }

        public async Task Update(int id, string title, string body, int userId)
        {
            PostBody postBody = new PostBody
            {
                id = id,
                title = title,
                body = body,
                userId = userId
            };
           
            RestRequest restRequest = new RestRequest($"{url}/{id}", Method.Patch);

            var response = await _restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content;
                Console.WriteLine(response);
            }
        }

        public async Task Delete(int id)
        {
            RestRequest restRequest = new RestRequest($"{url}/{id}", Method.Delete);
            var response = await _restClient.ExecuteAsync(restRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content;
                Console.WriteLine(response);
            }
        }
    }
}
