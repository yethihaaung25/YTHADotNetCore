using YTHADotNetCore.Shared;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace YTHADotNetCore.WinFormsAppSQLInjection
{
    public partial class Form1 : Form
    {
        private readonly DapperService _dapperService;
        public Form1()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //string query = $"SELECT * FROM tbl_User WHERE Email = '{txtEmail.Text.Trim()}' AND Password = '{txtPassword.Text.Trim()}'";
            string query = $"SELECT * FROM tbl_User WHERE Email = @Email AND Password = @Password";
            var model = _dapperService.QueryFirstOrDefault<UserModel>(query, new
            {
                Email = txtEmail.Text.Trim(),
                Password = txtPassword.Text.Trim()
            });
            if (model is null) 
            {
                MessageBox.Show("User Not Found.");
                return;
            }

            MessageBox.Show("IsAdmin : " + txtEmail.Text);
        }

    }

    public class UserModel
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
