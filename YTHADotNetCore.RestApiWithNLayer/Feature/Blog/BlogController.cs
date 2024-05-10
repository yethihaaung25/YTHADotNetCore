using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YTHADotNetCore.RestApiWithNLayer.Models;

namespace YTHADotNetCore.RestApiWithNLayer.Feature.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blogs _blBlogs;
        public BlogController()
        {
            _blBlogs = new BL_Blogs();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var lst = _blBlogs.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _blBlogs.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blogModel)
        {
            int result = _blBlogs.CreateBlog(blogModel);
            string message = result > 0 ? "Save Success" : "Save Fail";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blogModel)
        {
            var item = _blBlogs.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            int result = _blBlogs.UpdateBlogs(id, blogModel);
            string message = result > 0 ? "Update Success" : "Update Fail";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogModel blogModel)
        {
            var item = _blBlogs.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            int result = _blBlogs.PatchBlogs(id, blogModel);
            string message = result > 0 ? "Update Success" : "Update Fail";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _blBlogs.GetBlog(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            int result = _blBlogs.DeleteBlogs(id);
            string message = result > 0 ? "Delete Success" : "Delete Fail";
            return Ok(message);
        }
    }
}
