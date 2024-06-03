using System.Data.SqlClient;

namespace YTHADotNetCore.WinFormsAppSQLInjection
{
    internal static class ConnectionStrings
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "ACE\\SQLEXPRESS",
            InitialCatalog = "DotNetBatch4",
            UserID = "sa",
            Password = "as",
            TrustServerCertificate = true,
        };
    }
}
