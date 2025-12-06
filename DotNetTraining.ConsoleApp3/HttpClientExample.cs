using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTraining.ConsoleApp3
{
    public class HttpClientExample
    {
        private readonly HttpClient _httpClient;
        private readonly string url = "https://jsonplaceholder.typicode.com/posts";

        public HttpClientExample()
        {
            _httpClient = new HttpClient();
        }

        public async Task Get()
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode!)
            {
                Console.WriteLine("No data found");
            }
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }

        public async Task Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{url}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found");
            }

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
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
            var requestBody =  JsonConvert.SerializeObject(postBody);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(requestBody);
            }
        }

        public async Task Update(int id,string title, string body, int userId)
        {
            PostBody postBody = new PostBody
            {
                id = id,
                title = title,
                body = body,
                userId = userId
            };
            var requestBody =  JsonConvert.SerializeObject(postBody);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{url}/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response);
            }
        }

        public async Task Delete(int id)
        {
           
            var response = await _httpClient.DeleteAsync($"{url}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response);
            }
        }
    }


    public class PostBody
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

}
