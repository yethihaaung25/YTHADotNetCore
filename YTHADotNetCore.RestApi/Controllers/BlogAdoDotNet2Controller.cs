using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using YTHADotNetCore.RestApi.Models;
using YTHADotNetCore.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace YTHADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _doDotNetService = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM Tbl_Blog";
            var lst = _doDotNetService.Query<BlogModel>(query);
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

            int result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogTitle",blog.BlogTitle), new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor), new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            string message = result > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id,BlogModel blog)
        {
            var item = BlogById(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }
            string query = @"UPDATE [dbo].[Tbl_Blog]
            SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
              ,[BlogContent] = @BlogContent
            WHERE BlogId = @BlogId"
            ;

            blog.BlogId = id;
            int result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", blog.BlogId),new AdoDotNetParameter("@BlogTitle", blog.BlogTitle), new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor), new AdoDotNetParameter("@BlogContent", blog.BlogContent));
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

            condition = condition.Substring(0, condition.Length-2);
            string query = $@"UPDATE [dbo].[Tbl_Blog]
            SET {condition}
            WHERE BlogId = @BlogId"
            ;

            int result = 0;
            blog.BlogId = id;

            if (!string.IsNullOrEmpty(blog.BlogTitle) && string.IsNullOrEmpty(blog.BlogAuthor) && string.IsNullOrEmpty(blog.BlogContent))
            {
                result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", blog.BlogId),new AdoDotNetParameter("@BlogTitle", blog.BlogTitle));
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle) && !string.IsNullOrEmpty(blog.BlogAuthor) && string.IsNullOrEmpty(blog.BlogContent))
            {
                result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", blog.BlogId), new AdoDotNetParameter("@BlogTitle", blog.BlogTitle), new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor));
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle) && !string.IsNullOrEmpty(blog.BlogAuthor) && !string.IsNullOrEmpty(blog.BlogContent))
            {
                result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", blog.BlogId), new AdoDotNetParameter("@BlogTitle", blog.BlogTitle), new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor), new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor) && string.IsNullOrEmpty(blog.BlogTitle) && string.IsNullOrEmpty(blog.BlogContent))
            {
                result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", blog.BlogId),new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor));
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor) && !string.IsNullOrEmpty(blog.BlogContent) && string.IsNullOrEmpty(blog.BlogTitle))
            {
                result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", blog.BlogId), new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor), new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            }

            if (!string.IsNullOrEmpty(blog.BlogContent) && string.IsNullOrEmpty(blog.BlogTitle) && string.IsNullOrEmpty(blog.BlogAuthor))
            {
                result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", blog.BlogId),new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle) && !string.IsNullOrEmpty(blog.BlogContent) && string.IsNullOrEmpty(blog.BlogAuthor))
            {
                result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", blog.BlogId), new AdoDotNetParameter("@BlogTitle", blog.BlogTitle), new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            }

            //int result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", blog.BlogId), new AdoDotNetParameter("@BlogTitle", blog.BlogTitle), new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor), new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = BlogById(id);
            if (item is null)
            {
                return NotFound("No Data Found.");
            }


            string query = @"DELETE FROM [dbo].[Tbl_Blog]
            WHERE BlogId = @BlogId";

            int result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogId",id));
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }

        private BlogModel BlogById(int id)
        {
            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";
            var item = _doDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));
            return item;
        }
    }
}
