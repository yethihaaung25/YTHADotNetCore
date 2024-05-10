﻿using YTHADotNetCore.RestApiWithNLayer.Database;
using YTHADotNetCore.RestApiWithNLayer.Models;

namespace YTHADotNetCore.RestApiWithNLayer.Feature.Blog
{
    public class DA_Blogs
    {
        private readonly AppDbContext _context;
        public DA_Blogs() 
        {
            _context = new AppDbContext();
        }

        public List<BlogModel> GetBlogs()
        {
            var lst = _context.Blogs.ToList();
            return lst;
        }

        public BlogModel GetBlog(int id) 
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            return item;
        }

        public int CreateBlog(BlogModel requestModel) 
        {
            _context.Blogs.Add(requestModel);
            var result = _context.SaveChanges();
            return result;
        }

        public int UpdateBlog(int id, BlogModel requestModel) 
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null) return 0;

            item.BlogAuthor = requestModel.BlogAuthor;
            item.BlogTitle = requestModel.BlogTitle;
            item.BlogContent = requestModel.BlogContent;

            var reult = _context.SaveChanges();
            return reult;
        }

        public int PathcBlogs(int id, BlogModel requestModel)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null) return 0;

            if(!string.IsNullOrEmpty(requestModel.BlogTitle)) item.BlogTitle = requestModel.BlogTitle;
            if (!string.IsNullOrEmpty(requestModel.BlogAuthor)) item.BlogAuthor = requestModel.BlogAuthor;
            if (!string.IsNullOrEmpty(requestModel.BlogContent)) item.BlogContent = requestModel.BlogContent;

            var result = _context.SaveChanges();
            return result;
        }

        public int Delete(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null) return 0;

            _context.Blogs.Remove(item);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
