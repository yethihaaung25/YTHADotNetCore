using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using YTHADotNetCore.RestApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace YTHADotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM Tbl_Blog";
            SqlConnection con = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            con.Close();

            //List<BlogModel> lst = new List<BlogModel>();
            //foreach(DataRow dr  in dt.Rows)
            //{
            //    BlogModel blog = new BlogModel();
            //    blog.BlogId = Convert.ToInt32(dr["BlogId"]);
            //    blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
            //    blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
            //    blog.BlogContent = Convert.ToString(dr["BlogContent"]);

            //    BlogModel blog = new BlogModel()
            //    {
            //        BlogId = Convert.ToInt32(dr["BlogId"]),
            //        BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //        BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //        BlogContent = Convert.ToString(dr["BlogContent"]),
            //    };
            //    lst.Add(blog);
            //}

            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel 
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
            }).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";
            SqlConnection con = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@BlogId",id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            if(dt.Rows.Count == 0)
            {
                return NotFound("No Data Found.");
            }

            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"]),
            };
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

            SqlConnection con = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@BlogTitle",blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            var resutl = cmd.ExecuteNonQuery();
            con.Close();
            string message = resutl > 0 ? "Saving Successful" : "Saving Failed";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id,BlogModel blog)
        {
            var count = BlogById(id);
            if(count == 0) 
            {
                return NotFound("No Data Found.");
            }
            string query = @"UPDATE [dbo].[Tbl_Blog]
            SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
              ,[BlogContent] = @BlogContent
            WHERE BlogId = @BlogId"
            ;

            SqlConnection con = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            int result = cmd.ExecuteNonQuery();
            con.Close();
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id,BlogModel blog)
        {
            var count = BlogById(id);
            if (count == 0)
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

            SqlConnection con = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if(!string.IsNullOrEmpty(blog.BlogTitle))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blog.BlogTitle);
            }
            
            if(!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.BlogAuthor);
            }
            
            if(!string.IsNullOrEmpty(blog.BlogContent))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blog.BlogContent);
            }
            int result = cmd.ExecuteNonQuery();
            con.Close();
            string message = result > 0 ? "Update Successful" : "Update Failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var count = BlogById(id);
            if (count == 0)
            {
                return NotFound("No Data Found.");
            }


            string query = @"DELETE FROM [dbo].[Tbl_Blog]
            WHERE BlogId = @BlogId";

            SqlConnection con = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BlogId",id);
            var result = cmd.ExecuteNonQuery();
            con.Close();
            string message = result > 0 ? "Delete Successful" : "Delete Failed";
            return Ok(message);
        }

        private int BlogById(int id)
        {
            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";
            SqlConnection con = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            return dt.Rows.Count;
        }
    }
}
