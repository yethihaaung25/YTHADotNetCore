using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using YTHADotNetCore.RestApi.Models;
using YTHADotNetCore.Shared;

namespace YTHADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DapperService _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT  *  FROM Tbl_Blog";

            var lst = _dapperService.Query<BlogModel>(query);
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = BlogById(id);
            if(item is null)
            {
                return NotFound("No Data Found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
            VALUES
           (
           @BlogTitle,
		   @BlogAuthor,
		   @BlogContent
           )";

            int result = _dapperService.Execute(query, blog) ;
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id,BlogModel blog)
        {
            var item = BlogById(id);
            if(item is null)
            {
                return NotFound("No Data Found.");
            }

            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Tbl_Blog]
            SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
              ,[BlogContent] = @BlogContent
            WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, blog) ;
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id,BlogModel blog)
        {

            var item = BlogById(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }

            var condition = string.Empty;
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                condition += "[BlogContent] = @BlogContent, ";
            }

            if(condition.Length == 0)
            {
                return NotFound("No Data Found to Update.");
            }

            condition = condition.Substring(0,condition.Length - 2);

            blog.BlogId = id;
            string query = $@"UPDATE [dbo].[Tbl_Blog]
            SET {condition}
            WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, blog);
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = BlogById(id);
            if(item is null)
            {
                return NotFound("No Data Found.");
            }
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
            WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new BlogModel { BlogId = id });
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }

        private BlogModel? BlogById(int id)
        {
            string query = "SELECT  *  FROM Tbl_Blog WHERE  BlogId = @BlogId";

            var item = _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id});
            return item;
        }
    }
}
