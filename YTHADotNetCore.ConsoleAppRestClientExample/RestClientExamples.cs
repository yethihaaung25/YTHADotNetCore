using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YTHADotNetCore.ConsoleAppHTTPClientExample;

namespace YTHADotNetCore.ConsoleAppRestClientExample
{
    public class RestClientExamples
    {
        private readonly RestClient _client = new RestClient(new Uri("https://localhost:7013"));
        private readonly string _endPoint = "api/Blog";

        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await EditAsync(100);
            //await CreateAsync("Ye Thiha Aung","Ye Thiha Aung", "Ye Thiha Aung");
            //await UpdateAsync(18, ".Net Core", "Ye Thiha Aung", "Asp.Net");
            //await PatchBlogAsync(18, "Ye Ye", "", "Ye Ye");
            await DeleteAsync(18);
        }

        private async Task ReadAsync()
        {
            RestRequest restRequest = new RestRequest(_endPoint,Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if(response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;
                foreach(var item in lst)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(item));
                    Console.WriteLine($"Blog Title => {item.BlogTitle}");
                    Console.WriteLine($"Blog Author => {item.BlogAuthor}");
                    Console.WriteLine($"Blog Content => {item.BlogContent}");
                }
            }
        }

        private async Task EditAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_endPoint}/{id}",Method.Get);
            var response = await _client.ExecuteAsync(restRequest);
            if(response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                var item = JsonConvert.DeserializeObject<BlogDto>(jsonStr)!;
                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"Blog Title => {item.BlogTitle}");
                Console.WriteLine($"Blog Author => {item.BlogAuthor}");
                Console.WriteLine($"Blog Content => {item.BlogContent}");
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogDto blog = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            RestRequest restRequest = new RestRequest(_endPoint, Method.Post);
            restRequest.AddBody(blog);
            var response = await _client.ExecuteAsync(restRequest);
            if(response.IsSuccessStatusCode)
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            BlogDto blog = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            RestRequest restRequest = new RestRequest($"{_endPoint}/{id}",Method.Put);
            restRequest.AddBody(blog);
            var response = await _client.ExecuteAsync(restRequest);
            if(response.IsSuccessStatusCode) 
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task PatchBlogAsync(int id, string title, string author, string content)
        {
            BlogDto blog = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            RestRequest restRequest = new RestRequest($"{_endPoint}/{id}" ,Method.Patch);
            restRequest.AddBody(blog);
            var response = await _client.ExecuteAsync(restRequest);
            if(response.IsSuccessStatusCode) 
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }

        private async Task DeleteAsync(int id)
        {
            RestRequest restRequest = new RestRequest($"{_endPoint}/{id}" ,Method.Delete);
            var response = await _client.ExecuteAsync(restRequest);
            if(response.IsSuccessStatusCode) 
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
            else
            {
                string message = response.Content!;
                Console.WriteLine(message);
            }
        }
    }
}
