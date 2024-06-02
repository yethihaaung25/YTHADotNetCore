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
            List<BlogModel> list = _dapperService.Query<BlogModel>(BlogQuery.BlogList);
            dgvBlogList.DataSource = list;
        }
    }
}
