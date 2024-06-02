using System.Data.SqlClient;
using YTHADotNetCore.Shared;
using YTHADotNetCore.WinFormsApp.Models;
using YTHADotNetCore.WinFormsApp.Query;

namespace YTHADotNetCore.WinFormsApp
{
    public partial class frmBlog : Form
    {
        private readonly DapperService _dapperService;
        public frmBlog()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel();
                blog.BlogTitle = txtTitle.Text.Trim();
                blog.BlogAuthor = txtAuthor.Text.Trim();
                blog.BlogContent = txtContent.Text.Trim();

                int result = _dapperService.Execute(BlogQuery.BlogCreate,blog);
                string message = result > 0 ? "Save Successful." : "Save Fail!!!"; 
                var messageIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
                MessageBox.Show(message,"Blog",MessageBoxButtons.OK,messageIcon);
                if(result > 0) 
                {
                    ClearControl();
                }
            }
            catch(Exception ex) 
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
    }
}
