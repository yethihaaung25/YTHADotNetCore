// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;
using YTHADotNetCore.ConsoleApp.EfCoreExample;

Console.WriteLine("Hello, World!");

//SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
//stringBuilder.DataSource = "ACE\\SQLEXPRESS";
//stringBuilder.InitialCatalog = "DotNetBatch4";
//stringBuilder.UserID = "sa";
//stringBuilder.Password = "as";
//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
//connection.Open();
//Console.WriteLine("Connection open.");

//string query = "SELECT * FROM Tbl_Blog";
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//sqlDataAdapter.Fill(dt);

//connection.Close();
//Console.WriteLine("Connection Close");

//foreach(DataRow dr in dt.Rows)
//{
//    Console.WriteLine("Blog Id => " + dr["BlogId"]);
//    Console.WriteLine("Blog Title => " + dr["BlogTitle"]);
//    Console.WriteLine("Blog Author => " + dr["BlogAuthor"]);
//    Console.WriteLine("Blog Content => " + dr["BlogContent"]);
//    Console.WriteLine("-----------------------------------------");
//}

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Create("title 1","author 1","content 1");
//adoDotNetExample.Update(6,"title 10", "author 10", "content 10");
//adoDotNetExample.Delete(6);
//adoDotNetExample.Edit(5);

//DapperExample dapper = new DapperExample();
//dapper.Run();

EfCoreExample efCoreExample = new EfCoreExample();
efCoreExample.Run();
Console.Read();
