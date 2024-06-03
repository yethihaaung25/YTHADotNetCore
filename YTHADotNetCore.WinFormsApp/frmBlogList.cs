using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTHADotNetCore.Shared;
using YTHADotNetCore.WinFormsApp.Models;
using YTHADotNetCore.WinFormsApp.Query;

namespace YTHADotNetCore.WinFormsApp
{
    public partial class frmBlogList : Form
    {
        private readonly DapperService _dapperService;
        public frmBlogList()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        private void frmBlogList_Load(object sender, EventArgs e)
        {
            BlogList();
        }

        private void BlogList()
        {
            List<BlogModel> list = _dapperService.Query<BlogModel>(BlogQuery.BlogList);
            dgvBlogList.DataSource = list;
        }

        private void dgvBlogList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            var blogId = Convert.ToInt32(dgvBlogList.Rows[e.RowIndex].Cells["colId"].Value);

            if (e.ColumnIndex == (int)EnumFormControlType.Edit) 
            {
                frmBlog frm = new frmBlog(blogId);
                frm.ShowDialog();

                BlogList();
            }
            else if (e.ColumnIndex == (int)EnumFormControlType.Delete) 
            {
                var confirmDialogue = MessageBox.Show("Do you want to delete?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (confirmDialogue != DialogResult.Yes) return;
                BlogDelete(blogId);
                BlogList();
            }
        }

        private void BlogDelete(int blogId)
        {
            string query = "DELETE FROM tbl_Blog WHERE BlogId = @BlogId";

            var result = _dapperService.Execute(query, new BlogModel { BlogId = blogId});
            var message = result > 0 ? "Delete Successful." : "Delete Fail!!!";
            MessageBox.Show(message);
        }
    }
}
