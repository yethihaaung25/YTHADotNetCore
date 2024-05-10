using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YTHADotNetCore.RestApi.Database;
using YTHADotNetCore.RestApi.Models;

namespace YTHADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public BlogController()
        {
            _dbContext = new AppDbContext();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var item = _dbContext.Blogs.ToList();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blogModel)
        {
            _dbContext.Blogs.Add(blogModel);
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Save Success" : "Save Fail";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,BlogModel blogModel)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            item.BlogTitle = blogModel.BlogTitle;
            item.BlogAuthor = blogModel.BlogAuthor;
            item.BlogContent = blogModel.BlogContent;
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Update Success" : "Update Fail";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blogModel)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            if(!string.IsNullOrEmpty(blogModel.BlogTitle))
            {
                item.BlogTitle = blogModel.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blogModel.BlogAuthor))
            {
                item.BlogAuthor = blogModel.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blogModel.BlogContent))
            {
                item.BlogContent = blogModel.BlogContent;
            }
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Update Success" : "Update Fail";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            _dbContext.Blogs.Remove(item);
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Delete Success" : "Delete Fail";
            return Ok(message);
        }
    }
}
