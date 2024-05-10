using YTHADotNetCore.RestApiWithNLayer.Models;

namespace YTHADotNetCore.RestApiWithNLayer.Feature.Blog
{
    public class BL_Blogs
    {
        private readonly DA_Blogs _daBlogs;
        public BL_Blogs ()
        {
            _daBlogs = new DA_Blogs ();
        }

        public List<BlogModel> GetBlogs()
        {
            var lst = _daBlogs.GetBlogs();
            return lst;
        }

        public BlogModel GetBlog(int id) 
        {
            var item = _daBlogs.GetBlog(id);
            return item;
        }

        public int CreateBlog(BlogModel requestModel)
        {
            var item = _daBlogs.CreateBlog(requestModel);
            return item;
        }

        public int UpdateBlogs(int id, BlogModel model)
        {
            var result = _daBlogs.UpdateBlog(id, model);
            return result;
        }

        public int PatchBlogs(int id, BlogModel model)
        {
            var result = _daBlogs.PathcBlogs(id, model);
            return result;
        }

        public int DeleteBlogs(int id) 
        {
            var result = _daBlogs.Delete(id);
            return result;
        }
    }
}
