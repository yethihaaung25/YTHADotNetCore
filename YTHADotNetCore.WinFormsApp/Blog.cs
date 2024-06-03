using System.Data.SqlClient;
using YTHADotNetCore.Shared;
using YTHADotNetCore.WinFormsApp.Models;
using YTHADotNetCore.WinFormsApp.Query;

namespace YTHADotNetCore.WinFormsApp
{
    public partial class frmBlog : Form
    {
        private readonly DapperService _dapperService;
        private readonly int _blogId;
        public frmBlog()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        public frmBlog(int blogId)
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            _blogId = blogId;

            var model = _dapperService.QueryFirstOrDefault<BlogModel>("SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId", new BlogModel { BlogId = blogId });
            txtTitle.Text = model.BlogTitle;
            txtAuthor.Text = model.BlogAuthor;
            txtContent.Text = model.BlogContent;

            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel();
                blog.BlogTitle = txtTitle.Text.Trim();
                blog.BlogAuthor = txtAuthor.Text.Trim();
                blog.BlogContent = txtContent.Text.Trim();

                int result = _dapperService.Execute(BlogQuery.BlogCreate, blog);
                string message = result > 0 ? "Save Successful." : "Save Fail!!!";
                var messageIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show(message, "Blog", MessageBoxButtons.OK, messageIcon);
                if (result > 0)
                {
                    ClearControl();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        private void ClearControl()
        {
            txtTitle.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtContent.Text = string.Empty;

            txtTitle.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                var item = new BlogModel
                {
                    BlogId = _blogId,
                    BlogTitle = txtTitle.Text.Trim(),
                    BlogAuthor = txtAuthor.Text.Trim(),
                    BlogContent = txtContent.Text.Trim(),
                };

                string query = @"UPDATE [dbo].[Tbl_Blog]
                                SET [BlogTitle] = @BlogTitle
                                  ,[BlogAuthor] = @BlogAuthor
                                  ,[BlogContent] = @BlogContent
                                WHERE BlogId = @BlogId";

                var result = _dapperService.Execute(query, item);
                string message = result > 0 ? "Update Successful" : "Update Failed";
                MessageBox.Show(message);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
