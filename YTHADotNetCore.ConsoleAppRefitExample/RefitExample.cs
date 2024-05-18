using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace YTHADotNetCore.ConsoleAppRefitExample
{
    public class RefitExample
    {
        private readonly IBlogApi _blogApi = RestService.For<IBlogApi>("https://localhost:7295");

        public async Task RunAsync()
        {
            //await ReadAsync();
            //await EditAsync(1);
            //await EditAsync(100);
            //await CreateAsync("Title17","Author17","Content17");
            //await UpdateAsync(19,"Title1", "Author7", "Content17");
            //await PathchAsync(19,"","author","");
            await DeleteAsync(19);
        }

        private async Task ReadAsync()
        {
            var lst = await _blogApi.GetBlogs();
            foreach (var item in lst) 
            {
                Console.WriteLine($"BlogId => {item.BlogId}");
                Console.WriteLine($"BlogTitle => {item.BlogTitle}");
                Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
                Console.WriteLine($"BlogContent => {item.BlogContent}");
                Console.WriteLine("-------------------------------------");
            }
        }

        private async Task EditAsync(int id)
        {
            try
            {
                var item = await _blogApi.GetBlog(id);
                Console.WriteLine($"BlogId => {item.BlogId}");
                Console.WriteLine($"BlogTitle => {item.BlogTitle}");
                Console.WriteLine($"BlogAuthor => {item.BlogAuthor}");
                Console.WriteLine($"BlogContent => {item.BlogContent}");
                Console.WriteLine("-------------------------------------");
            }
            catch(ApiException ex)
            {
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CreateAsync(string title, string author, string content)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            var message = await _blogApi.CreateBlog(blog);
            Console.WriteLine(message);
        }

        private async Task UpdateAsync(int id, string title, string author, string content)
        {
            try
            {
                BlogModel blog = new BlogModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                };
                var message = await _blogApi.UpadateBlog(id, blog);
                Console.WriteLine(message);
            }
            catch(ApiException ex)
            {
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task PathchAsync(int id, string title, string author, string content)
        {
            try
            {
                var item = await _blogApi.GetBlog(id);
                if(!string.IsNullOrEmpty(title)) item.BlogTitle = title;
                if (!string.IsNullOrEmpty(author)) item.BlogAuthor = author;
                if (!string.IsNullOrEmpty(content)) item.BlogContent = content;
                var message = await _blogApi.PatchBlog(id, item);
                Console.WriteLine(message);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task DeleteAsync(int id)
        {
            try
            {
                var message = await _blogApi.DeleteBlog(id);
                Console.WriteLine(message);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
